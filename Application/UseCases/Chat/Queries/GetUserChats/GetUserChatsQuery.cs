using Application.Dtos.Chat;
using Application.Dtos.Page;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetUserChats;

public sealed record GetUserChatsQuery(Guid UserId, PageData PageData) : IRequest<PagedResponse<ChatReadDto>>;