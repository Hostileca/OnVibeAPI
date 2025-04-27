using Application.Dtos.Message;
using Application.Dtos.Page;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Message.Queries.GetChatMessages;

public class GetChatMessagesQuery : RequestBase<PagedResponse<MessageReadDto>>
{
    public Guid ChatId { get; init; }
    public PageData PageData { get; init; }
}