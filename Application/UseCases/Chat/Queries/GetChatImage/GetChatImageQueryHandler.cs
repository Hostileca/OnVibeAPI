using Application.Helpers;
using Application.Helpers.PermissionsHelpers;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatImage;

public class GetChatImageQueryHandler(
    IChatRepository chatRepository)
    : IRequestHandler<GetChatImageQuery, byte[]>
{
    public async Task<byte[]> Handle(GetChatImageQuery request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId,
            new ChatIncludes{ IncludeChatMembers = true },
            cancellationToken);
        
        if(chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }
        
        if (!ChatPermissionsHelper.IsUserHasAccessToChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have access to this chat");
        }
        
        if (chat.Image is null)
        {
            throw new NotFoundException("Image is null");
        }
        
        return chat.Image is null ? [] : Base64Converter.ConvertToByteArray(chat.Image);
    }
}