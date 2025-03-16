using Application.Dtos.Message;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Message.Commands.SendMessage;

public class SendMessageCommand : IRequest<MessageReadDtoBase>
{
    public Guid ChatId { get; set; }
    public string? Text { get; set; }
    public Guid InitiatorId { get; set; }
    public IList<IFormFile>? Attachments { get; set; }
    public DateTimeOffset? Delay { get; set; }
}