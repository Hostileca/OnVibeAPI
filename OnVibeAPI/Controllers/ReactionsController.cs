﻿using Application.UseCases.Reaction.Commands.UpsertReaction;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Reaction;

namespace OnVibeAPI.Controllers;

public class ReactionsController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> UpsertReaction([FromBody] UpsertReactionRequest upsertReactionRequest, CancellationToken cancellationToken)
    {
        var command = new UpsertReactionCommand
        {
            InitiatorId = InitiatorId,
            ChatId = upsertReactionRequest.ChatId,
            MessageId = upsertReactionRequest.MessageId,
            Emoji = upsertReactionRequest.Emoji
        };
        
        await mediator.Send(command, cancellationToken);
        
        return Accepted();
    }
}