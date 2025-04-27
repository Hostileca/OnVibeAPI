using Application.Dtos.Chat;
using Application.Dtos.Page;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetUserChats;

public sealed class GetUserChatsQuery : IRequest<PagedResponse<ChatReadDto>>
{
    public Guid InitiatorId { get; init; }
    public PageData PageData { get; init; }
}