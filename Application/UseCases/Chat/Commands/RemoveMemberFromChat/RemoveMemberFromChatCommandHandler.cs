using Application.Dtos.Chat;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Commands.RemoveMemberFromChat;

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

        var initiatorToChat = chat.ChatsMembers.FirstOrDefault(m => m.UserId == request.InitiatorId);

        if (initiatorToChat is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }
        
        if (initiatorToChat.Role == ChatRoles.Member)
        {
            throw new ForbiddenException("You don't have permissions to add members to this chat");
        }
        
        var userToChat = chat.ChatsMembers.FirstOrDefault(m => m.UserId == request.UserId);

        if (userToChat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }
        
        chat.ChatsMembers.Remove(userToChat);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}