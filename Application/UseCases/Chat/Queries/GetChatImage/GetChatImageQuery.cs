using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatImage;

public class GetChatImageQuery : IRequest<byte[]>
{
    public Guid ChatId { get; init; }
    public Guid InitiatorId { get; init; }
}