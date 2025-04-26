using Application.Dtos.Message;
using Application.Dtos.Page;
using MediatR;

namespace Application.UseCases.Message.Queries.GetChatMessages;

public class GetChatMessagesQuery : IRequest<PagedResponse<MessageReadDto>>
{
    public Guid ChatId { get; set; }
    public Guid InitiatorId { get; set; }
    public PageData PageData { get; set; }
}