using Application.Dtos.Page;
using Application.UseCases.Comment.Commands.SendCommentToPost;
using Application.UseCases.Comment.Queries.GetPostComments;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Comment;
using OnVibeAPI.Requests.General;

namespace OnVibeAPI.Controllers;

public class CommentsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendComment([FromForm] SendCommentRequest createCommentRequest, CancellationToken cancellationToken)
    {
        var command = new SendCommentToPostCommand{ Content = createCommentRequest.Content, PostId = createCommentRequest.PostId };
        
        return Ok(await mediator.Send(command, cancellationToken));
    }
    
    [HttpGet]
    public async Task<IActionResult> SendComment([FromQuery] Guid postId, 
        [FromQuery] PageRequest pageRequest, 
        CancellationToken cancellationToken)
    {
        var query = new GetPostCommentsQuery{ PostId = postId, PageData = pageRequest.Adapt<PageData>() };
        
        return Ok(await mediator.Send(query, cancellationToken));
    }
}