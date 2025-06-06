﻿using Application.Dtos.Page;
using Application.UseCases.Post.Commands.Create;
using Application.UseCases.Post.Queries.GetUserPosts;
using Application.UseCases.Post.Queries.GetUserWall;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.General;
using OnVibeAPI.Requests.Post;

namespace OnVibeAPI.Controllers;

public class PostsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePostCommand{InitiatorId = InitiatorId, Content = request.Content, Attachments = request.Attachments};
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserPosts(
        [FromQuery] Guid userId, 
        [FromQuery] PageRequest pageRequest, 
        CancellationToken cancellationToken)
    {
        var query = new GetUserPostsQuery{ PageData = pageRequest.Adapt<PageData>(), UserId = userId };
        
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("wall")]
    public async Task<IActionResult> GetUserPosts(
        [FromQuery] PageRequest pageRequest, 
        CancellationToken cancellationToken)
    {
        var query = new GetUserWallQuery{ PageData = pageRequest.Adapt<PageData>(), UserId = InitiatorId };
        
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}