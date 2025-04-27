using Application.Dtos.Message;
using Application.UseCases.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Message.Commands.SendMessage;

public class SendMessageCommand : RequestBase<MessageReadDtoBase>
{
    public Guid ChatId { get; init; }
    public string? Text { get; init; }
    public IList<IFormFile>? Attachments { get; init; }
    public DateTimeOffset? Delay { get; init; }
}