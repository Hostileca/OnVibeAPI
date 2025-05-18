using Application.Dtos.Chat;
using Application.Dtos.Message;
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
            new ChatIncludes
            {
                IncludeChatMembers = true
            }, 
            cancellationToken,
            true);
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }

        var initiatorMember = chat.Members.FirstOrDefault(member => member.UserId == request.InitiatorId);
        if (initiatorMember is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }
        
        var currentDate = DateTime.UtcNow;
        if (request.InitiatorId == request.UserId)
        {
            if (initiatorMember.Role == ChatRole.Admin)
            {
                throw new ForbiddenException("You can't leave the chat as an admin");
            }

            var leaveMessage = new Domain.Entities.Message
            {
                Date = currentDate,
                ChatId = chat.Id,
                Text = GetLeaveMessage(initiatorMember.User)
            };

            initiatorMember.RemoveDate = currentDate;
            await messageRepository.AddAsync(leaveMessage, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken);

            await chatNotificationService.SendMessageToGroupAsync(leaveMessage.Adapt<MessageReadDto>(), cancellationToken);
            await chatNotificationService.RemoveMemberFromGroupAsync(initiatorMember, cancellationToken);

            return chat.Adapt<ChatReadDto>();
        }

        if (!ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to remove members from this chat");
        }

        var targetMember = chat.Members.FirstOrDefault(m => m.UserId == request.UserId);
        if (targetMember is null || targetMember.IsRemoved)
        {
            throw new NotFoundException(typeof(Domain.Entities.ChatMember), request.UserId.ToString());
        }

        await chatMembersRepository.LoadUser(initiatorMember, cancellationToken);
        await chatMembersRepository.LoadUser(targetMember, cancellationToken);
        var removeMessage = new Domain.Entities.Message
        {
            Date = currentDate,
            ChatId = chat.Id,
            Text = GetRemoveMessage(initiatorMember.User, targetMember.User)
        };

        targetMember.RemoveDate = currentDate;
        await messageRepository.AddAsync(removeMessage, cancellationToken);
        chat.Members.Remove(targetMember);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        await chatNotificationService.SendMessageToGroupAsync(removeMessage.Adapt<MessageReadDto>(), cancellationToken);
        await chatNotificationService.RemoveMemberFromGroupAsync(targetMember, cancellationToken);

        return chat.Adapt<ChatReadDto>();
    }
}