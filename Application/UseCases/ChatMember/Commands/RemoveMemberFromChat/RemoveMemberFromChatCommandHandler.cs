using Application.Dtos.Chat;
using Application.Helpers.PermissionsHelpers;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.RemoveMemberFromChat;

public class RemoveMemberFromChatCommandHandler(
    IChatRepository chatRepository)  
    : IRequestHandler<RemoveMemberFromChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(RemoveMemberFromChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId, 
            new ChatIncludes{ IncludeChatMembers = true }, 
            cancellationToken, 
            true);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }

        var initiatorToChat = chat.Members.FirstOrDefault(m => m.UserId == request.InitiatorId);

        if (initiatorToChat is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }

        if (request.InitiatorId == request.UserId)
        {
            if (initiatorToChat.Role == ChatRoles.Admin)
            {
                throw new ForbiddenException("You can't leave the chat as an admin");
            }
            
            chat.Members.Remove(initiatorToChat);
            await chatRepository.SaveChangesAsync(cancellationToken);
        }
        
        if (ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to add members to this chat");
        }
        
        var userToChat = chat.Members.FirstOrDefault(m => m.UserId == request.UserId);

        if (userToChat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.ChatMember), request.UserId.ToString());
        }
        
        chat.Members.Remove(userToChat);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}