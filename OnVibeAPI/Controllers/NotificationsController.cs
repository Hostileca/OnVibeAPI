using Application.UseCases.Notification.Commands.UpdateNotificationsByIds;
using Application.UseCases.Notification.Queries.GetUserNotifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Notification;

namespace OnVibeAPI.Controllers;

public class NotificationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetNotifications([FromQuery] bool isRead, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserNotificationsQuery(UserId, isRead), cancellationToken);
            
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateNotifications(UpdateNotificationsRequest updateNotificationsRequest, CancellationToken cancellationToken)
    {
        var command = new UpdateNotificationsByIdsCommand(
            updateNotificationsRequest.NotificationIds, 
            UserId, 
            updateNotificationsRequest.IsRead);
        
        await mediator.Send(command, cancellationToken);
            
        return Accepted();
    }
}