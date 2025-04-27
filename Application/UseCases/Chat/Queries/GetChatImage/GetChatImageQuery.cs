using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Chat.Queries.GetChatImage;

public class GetChatImageQuery : RequestBase<byte[]>
{
    public Guid ChatId { get; set; }
}