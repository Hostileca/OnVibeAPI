using Application.Dtos.Message;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Message.Commands.SendMessage;

public class SendMessageCommand : IRequest<MessageReadDtoBase>
{
    public Guid ChatId { get; init; }
    public Guid InitiatorId { get; init; }
    public string? Text { get; init; }
    public IList<IFormFile>? Attachments { get; init; }
    public DateTime? Date { get; init; }
}