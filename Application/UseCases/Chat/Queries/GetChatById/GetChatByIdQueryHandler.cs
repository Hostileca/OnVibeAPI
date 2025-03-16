using Application.Dtos.Chat;
using Application.Helpers.PermissionsHelpers;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatById;

public class GetChatByIdQueryHandler(
    IChatRepository chatRepository)
    : IRequestHandler<GetChatByIdQuery, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId,
            new ChatIncludes { IncludeChatMembers = true },
            cancellationToken);

        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }

        if (!ChatPermissionsHelper.IsUserHasAccessToChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have access to this chat");
        }
        
        return chat.Adapt<ChatReadDto>();
    }
}