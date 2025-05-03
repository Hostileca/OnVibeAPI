using Application.Dtos.Chat;
using Application.Helpers.PermissionsHelpers;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatCommandHandler(
    IChatRepository chatRepository,
    IChatMembersRepository chatMembersRepository,
    IMessageRepository messageRepository,
    IChatNotificationService chatNotificationService)  
    : IRequestHandler<RemoveMemberFromChatCommand, ChatReadDto>
{
    private static string GetLeaveMessage(Domain.Entities.User user) => $"{user.Username} has left the chat.";
    private static string GetRemoveMessage(Domain.Entities.User initiator, Domain.Entities.User user) => $"{user.Username} was removed by {initiator.Username}";

    public async Task<ChatReadDto> Handle(RemoveMemberFromChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId, 
            new ChatIncludes(), 
            cancellationToken);

        if (chat is null)
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());

        var initiatorMember = await chatMembersRepository.GetChatMemberAsync(
            request.InitiatorId,
            request.ChatId,
            new ChatMemberIncludes { IncludeUser = true },
            cancellationToken,
            true);

        if (initiatorMember is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }

        if (request.InitiatorId == request.UserId)
        {
            if (initiatorMember.Role == ChatRoles.Admin)
            {
                throw new ForbiddenException("You can't leave the chat as an admin");
            }

            var leaveMessage = new Domain.Entities.Message
            {
                Date = DateTime.UtcNow,
                ChatId = chat.Id,
                Text = GetLeaveMessage(initiatorMember.User)
            };

            chatMembersRepository.Remove(initiatorMember);
            await messageRepository.AddAsync(leaveMessage, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken);

            await chatNotificationService.SendMessageAsync(leaveMessage, cancellationToken);
            await chatNotificationService.RemoveMemberAsync(initiatorMember, cancellationToken);

            return chat.Adapt<ChatReadDto>();
        }

        if (!ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to remove members from this chat");
        }

        var targetMember = await chatMembersRepository.GetChatMemberAsync(
            request.UserId,
            request.ChatId,
            new ChatMemberIncludes { IncludeUser = true },
            cancellationToken,
            true);

        if (targetMember is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.ChatMember), request.UserId.ToString());
        }

        var removeMessage = new Domain.Entities.Message
        {
            Date = DateTime.UtcNow,
            ChatId = chat.Id,
            Text = GetRemoveMessage(initiatorMember.User, targetMember.User)
        };

        chatMembersRepository.Remove(targetMember);
        await messageRepository.AddAsync(removeMessage, cancellationToken);
        await chatRepository.SaveChangesAsync(cancellationToken);
        await chatNotificationService.SendMessageAsync(removeMessage, cancellationToken);
        await chatNotificationService.RemoveMemberAsync(targetMember, cancellationToken);

        return chat.Adapt<ChatReadDto>();
    }
}