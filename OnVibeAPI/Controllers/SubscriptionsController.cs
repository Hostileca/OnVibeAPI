using Application.UseCases.Subscription.UpsertSubscription;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnVibeAPI.Controllers;

public class SubscriptionsController(IMediator mediator) : ControllerBase
{
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> SubscribeToUser(Guid userId, CancellationToken cancellationToken)
    {
        var command = new UpsertSubscriptionCommand{ InitiatorId = InitiatorId, UserId = userId };
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}