using Application.Dtos.ChatMember;
using Application.Dtos.Message;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.SetRoleToMember;

public class SetRoleToMemberCommandHandler(
    IChatRepository chatRepository,
    IChatMembersRepository chatMembersRepository,
    IMessageRepository messageRepository,
    IChatNotificationService chatNotificationService)
    : IRequestHandler<SetRoleToMemberCommand, ChatMemberReadDto>
{
    private static string GetUpdateMessage(Domain.Entities.User initiator, Domain.Entities.User user, ChatRole chatRole) => $"{initiator.Username} set the {chatRole.ToString()} role to {user.Username}";
    
    public async Task<ChatMemberReadDto> Handle(SetRoleToMemberCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId, 
            new ChatIncludes(),
            cancellationToken,
            true);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }
        
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

        if (initiatorMember.Role != ChatRole.Admin)
        {
            throw new ForbiddenException("You don't have permissions to set roles to members of this chat");
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
        
        var updateMessage = new Domain.Entities.Message
        {
            Date = DateTime.UtcNow,
            ChatId = chat.Id,
            Text = GetUpdateMessage(initiatorMember.User, targetMember.User, request.Role)
        };
        
        targetMember.Role = request.Role;
        await messageRepository.AddAsync(updateMessage, cancellationToken);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        await chatNotificationService.SendMessageToGroupAsync(updateMessage.Adapt<MessageReadDto>(), cancellationToken);
        
        return targetMember.Adapt<ChatMemberReadDto>();
    }
}