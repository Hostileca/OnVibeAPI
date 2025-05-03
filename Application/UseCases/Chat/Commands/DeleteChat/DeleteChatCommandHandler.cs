using Application.Dtos.Chat;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Commands.DeleteChat;

public class DeleteChatCommandHandler(
    IChatRepository chatRepository,
    IExtraLoader<ChatReadDto> chatExtraLoader)
    : IRequestHandler<DeleteChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
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
        
        var initiatorToChat = chat.Members.FirstOrDefault(m => m.UserId == request.InitiatorId);
        
        if(initiatorToChat is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }
        
        if (initiatorToChat.Role != ChatRole.Admin)
        {
            throw new ForbiddenException("You don't have permissions to delete this chat");
        }

        var chatReadDto = chat.Adapt<ChatReadDto>();
        await chatExtraLoader.LoadExtraInformationAsync(chatReadDto, cancellationToken);
        
        chatRepository.RemoveChat(chat);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chatReadDto;
    }
}