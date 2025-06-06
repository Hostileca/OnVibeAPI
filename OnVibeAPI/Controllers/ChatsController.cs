﻿using Application.Dtos.Page;
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
        var command = new CreateChatCommand{
            InitiatorId = InitiatorId, 
            Name = createChatRequest.Name, 
            Image = createChatRequest.Image, 
            UserIds = createChatRequest.UserIds 
        };
        
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{chatId:guid}")]
    public async Task<IActionResult> UpdateChat(Guid chatId, [FromForm] UpdateChatRequest updateChatRequest,
        CancellationToken cancellationToken)
    {
        var command = new UpdateChatCommand{
            ChatId = chatId,
            InitiatorId = InitiatorId, 
            Name = updateChatRequest.Name, 
            Image = updateChatRequest.Image 
        };

        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpDelete("{chatId:guid}")]
    public async Task<IActionResult> DeleteChat(Guid chatId, CancellationToken cancellationToken)
    {
        var command = new DeleteChatCommand{ InitiatorId = InitiatorId, ChatId = chatId };

        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("my")]
    public async Task<IActionResult> GetMyChats([FromQuery]PageRequest pageRequest, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserChatsQuery{ InitiatorId = InitiatorId, PageData = pageRequest.Adapt<PageData>() }, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{chatId:guid}")]
    public async Task<IActionResult> GetChat(Guid chatId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetChatByIdQuery{ ChatId = chatId, InitiatorId = InitiatorId }, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{chatId:guid}/image")]
    public async Task<IActionResult> GetChatImage(Guid chatId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetChatImageQuery{ ChatId = chatId, InitiatorId = InitiatorId }, cancellationToken);
        
        return File(result, "image/jpeg");
    }
}