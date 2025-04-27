using Application.Dtos.Chat;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatById;

public class GetChatByIdQuery : IRequest<ChatReadDto>
{
    public Guid ChatId { get; init; }
    public Guid InitiatorId { get; init; }
}