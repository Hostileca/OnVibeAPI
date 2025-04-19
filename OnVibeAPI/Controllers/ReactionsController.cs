using Application.UseCases.Reaction.UpsertReaction;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Reaction;

namespace OnVibeAPI.Controllers;

public class ReactionsController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> UpsertReaction([FromForm] UpsertReactionRequest upsertReactionRequest, CancellationToken cancellationToken)
    {
        var command = upsertReactionRequest.Adapt<UpsertReactionCommand>();
        command.InitiatorId = UserId;
        
        await mediator.Send(command, cancellationToken);
        
        return Accepted();
    }
}