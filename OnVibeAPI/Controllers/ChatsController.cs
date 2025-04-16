using Application.Dtos.Page;
using Application.UseCases.Chat.Commands.CreateChat;
using Application.UseCases.Chat.Commands.DeleteChat;
using Application.UseCases.Chat.Commands.UpdateChat;
using Application.UseCases.Chat.Queries.GetChatById;
using Application.UseCases.Chat.Queries.GetChatImage;
using Application.UseCases.Chat.Queries.GetUserChats;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Chat;
using OnVibeAPI.Requests.General;

namespace OnVibeAPI.Controllers;

public class ChatsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateChat([FromForm] CreateChatRequest createChatRequest, 
        CancellationToken cancellationToken)
    {
        var command = createChatRequest.Adapt<CreateChatCommand>();
        command.InitiatorId = UserId;
        
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{chatId:guid}")]
    public async Task<IActionResult> UpdateChat(Guid chatId, [FromForm] UpdateChatRequest updateChatRequest,
        CancellationToken cancellationToken)
    {
        var command = new UpdateChatCommand
        {
            ChatId = chatId, 
            InitiatorId = UserId, 
            Image = updateChatRequest.Image, 
            Name = updateChatRequest.Name
        };

        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpDelete("{chatId:guid}")]
    public async Task<IActionResult> DeleteChat(Guid chatId, CancellationToken cancellationToken)
    {
        var command = new DeleteChatCommand(UserId, chatId);

        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("my")]
    public async Task<IActionResult> GetMyChats([FromQuery]PageRequest pageRequest, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserChatsQuery(UserId, pageRequest.Adapt<PageData>()), cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{chatId:guid}")]
    public async Task<IActionResult> GetChat(Guid chatId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetChatByIdQuery(chatId, UserId), cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{chatId:guid}/image")]
    public async Task<IActionResult> GetChatImage(Guid chatId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetChatImageQuery(chatId, UserId), cancellationToken);
        
        return File(result, "image/jpeg");
    }
}