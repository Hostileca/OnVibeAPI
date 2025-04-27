using Application.Dtos.Page;
using Application.UseCases.User.Commands.Login;
using Application.UseCases.User.Commands.Register;
using Application.UseCases.User.Commands.UpdateUserProfile;
using Application.UseCases.User.Queries.GetUserAvatar;
using Application.UseCases.User.Queries.GetUserById;
using Application.UseCases.User.Queries.GetUsersBySearchCriteria;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.General;
using OnVibeAPI.Requests.User;

namespace OnVibeAPI.Controllers;

public class UsersController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var command = registerRequest.Adapt<RegisterCommand>();

        return Ok(await mediator.Send(command, cancellationToken));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var command = loginRequest.Adapt<LoginCommand>();
        
        return Ok(await mediator.Send(command, cancellationToken));
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetUserByIdQuery{ Id = UserId }, cancellationToken));
    }
    
    [HttpGet("{id:guid}")] 
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        return Ok(await mediator.Send(new GetUserByIdQuery{ Id = id }, cancellationToken));
    }
    
    [HttpGet("{id:guid}/avatar")] 
    public async Task<IActionResult> GetUserAvatar([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserAvatarQuery{ UserId = id }, cancellationToken);
        
        return File(result, "image/jpeg");
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromForm] UpdateUserProfileRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserProfileCommand
        {
            InitiatorId = UserId,
            BIO = request.BIO,
            Avatar = request.Avatar,
            DateOfBirth = request.DateOfBirth,
            City = request.City
        };
        
        return Ok(await mediator.Send(command, cancellationToken));
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(
        [FromQuery] SearchUsersByCriteriaRequest searchRequest,
        [FromQuery] PageRequest pageRequest,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersBySearchCriteriaQuery
        {
            Username = searchRequest.Username,
            Country = searchRequest.Country,
            City = searchRequest.City,
            PageData = pageRequest.Adapt<PageData>()
        };

        return Ok(await mediator.Send(query, cancellationToken));
    }
}