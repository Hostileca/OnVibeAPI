using Application.UseCases.Like.Commands.AddLikeToPost;
using Application.UseCases.Like.Commands.RemoveLikeFromPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Like;

namespace OnVibeAPI.Controllers;

public class LikesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddLikeToPost([FromBody] AddLikeRequest addLikeRequest, CancellationToken cancellationToken)
    {
        var command = new AddLikeToPostCommand(addLikeRequest.PostId, UserId);
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteLikeFromPost([FromBody] DeleteLikeRequest deleteLikeRequest, CancellationToken cancellationToken)
    {
        var command = new RemoveLikeFromPostCommand(deleteLikeRequest.PostId, UserId);
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}