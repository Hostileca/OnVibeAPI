using Application.UseCases.Post.Commands.Create;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Post;

namespace OnVibeAPI.Controllers;

public class PostsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreatePostCommand>();
        command.UserId = UserId;
        command.Attachments = request.Attachments;
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}