using Application.Dtos.Chat;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatById;

public class GetChatByIdQuery : RequestBase<ChatReadDto>
{
    public Guid ChatId { get; init; }
}