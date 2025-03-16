using Application.UseCases.Subscription.SubscribeToUser;
using Application.UseCases.Subscription.UnsubscribeFromUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnVibeAPI.Controllers;

public class SubscriptionsController(IMediator mediator) : ControllerBase
{
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> SubscribeToUser(Guid userId, CancellationToken cancellationToken)
    {
        var command = new SubscribeToUserCommand(UserId, userId);
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> UnsubscribeFromUser(Guid userId, CancellationToken cancellationToken)
    {
        var command = new UnsubscribeFromUserCommand(UserId, userId);
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}