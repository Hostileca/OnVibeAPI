using Application.Dtos.Message;
using Application.Dtos.Page;
using MediatR;

namespace Application.UseCases.Message.Queries.GetChatMessages;

public class GetChatMessagesQuery : IRequest<PagedResponse<MessageReadDto>>
{
    public Guid ChatId { get; init; }
    public Guid InitiatorId { get; init; }
    public PageData PageData { get; init; }
}