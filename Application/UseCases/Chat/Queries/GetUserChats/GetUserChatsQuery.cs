using Application.Dtos.Chat;
using Application.Dtos.Page;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetUserChats;

public sealed class GetUserChatsQuery : RequestBase<PagedResponse<ChatReadDto>>
{
    public PageData PageData { get; init; }
}