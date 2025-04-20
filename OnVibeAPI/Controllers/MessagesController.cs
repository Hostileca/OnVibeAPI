using Application.Dtos.Page;
using Application.UseCases.Message.Commands.SendMessage;
using Application.UseCases.Message.Queries.GetChatMessages;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.General;
using OnVibeAPI.Requests.Message;

namespace OnVibeAPI.Controllers;

public class MessagesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromForm] SendMessageRequest sendMessageRequest,
        CancellationToken cancellationToken)
    {
        var command = new SendMessageCommand(
            sendMessageRequest.ChatId,
            sendMessageRequest.Text,
            UserId,
            sendMessageRequest.Attachments,
            sendMessageRequest.Delay);
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetChatMessages(
        [FromQuery] Guid chatId, 
        [FromQuery] PageRequest pageRequest, 
        CancellationToken cancellationToken)
    {
        var query = new GetChatMessagesQuery
        {
            ChatId = chatId,
            InitiatorId = UserId,
            PageData = pageRequest.Adapt<PageData>()
        };
        
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}