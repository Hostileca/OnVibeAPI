using Application.Dtos.Message;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Message.Commands.SendMessage;

public record SendMessageCommand(
    Guid ChatId, 
    string? Text, 
    Guid InitiatorId, 
    IList<IFormFile>? Attachments, 
    DateTimeOffset? Delay) : IRequest<MessageReadDtoBase>;