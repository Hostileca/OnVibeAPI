using Application.Dtos.Page;
using Application.Enums;
using Application.UseCases.Subscription.Commands.UpsertSubscription;
using Application.UseCases.Subscription.Queries.GetUserSubscribers;
using Application.UseCases.Subscription.Queries.GetUserSubscriptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.General;

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
    
    [HttpGet]
    public async Task<IActionResult> GetSubscriptions(Guid userId, [FromQuery] SubscriptionType type, [FromQuery] PageRequest pageRequest, CancellationToken cancellationToken)
    {
        switch (type)
        {
            case SubscriptionType.Incoming:
                var incomingQuery = new GetUserSubscribersQuery { 
                    UserId = userId, 
                    PageData = pageRequest.Adapt<PageData>() 
                };
                var incomingResult = await mediator.Send(incomingQuery, cancellationToken);
                return Ok(incomingResult);
            
            case SubscriptionType.Outgoing:
                var outgoingQuery = new GetUserSubscriptionsQuery { 
                    UserId = userId, 
                    PageData = pageRequest.Adapt<PageData>() 
                };
                var outgoingResult = await mediator.Send(outgoingQuery, cancellationToken);
                return Ok(outgoingResult);
            
            default:
                return BadRequest($"Unknown subscription type: {type}");
        }
    }
}