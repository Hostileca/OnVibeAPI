using Application.Dtos.Chat;
using Application.Helpers.PermissionsHelpers;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Commands.UpdateChat;

public class UpdateChatCommandHandler(
    IChatRepository chatRepository) 
    : IRequestHandler<UpdateChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(request.ChatId, 
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
        
        if (!ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to update this chat");
        }

        chat.Adapt(request);
        
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}