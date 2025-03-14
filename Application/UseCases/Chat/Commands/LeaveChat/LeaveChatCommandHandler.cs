using Application.Dtos.Chat;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Commands.LeaveChat;

public class LeaveChatCommandHandler(
    IChatRepository chatRepository) 
    : IRequestHandler<LeaveChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(LeaveChatCommand request, CancellationToken cancellationToken)
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
        
        chat.ChatsMembers.Remove(chat.ChatsMembers.FirstOrDefault(m => m.UserId == request.UserId));
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}