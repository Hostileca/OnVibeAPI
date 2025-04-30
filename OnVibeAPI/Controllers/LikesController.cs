using Application.UseCases.Like.Commands.UpsertLike;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Like;

namespace OnVibeAPI.Controllers;

public class LikesController(IMediator mediator) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> UpsertLike([FromBody] AddLikeRequest addLikeRequest, CancellationToken cancellationToken)
    {
        var command = new UpsertLikeCommand{ PostId = addLikeRequest.PostId, InitiatorId = InitiatorId };
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}