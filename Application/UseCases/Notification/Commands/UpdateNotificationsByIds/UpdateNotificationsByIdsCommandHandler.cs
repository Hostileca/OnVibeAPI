using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.UseCases.Notification.Commands.UpdateNotificationsByIds;

public sealed class UpdateNotificationsByIdsCommandHandler(
    INotificationRepository notificationRepository)
    : IRequestHandler<UpdateNotificationsByIdsCommand, Unit>
{
    public async Task<Unit> Handle(UpdateNotificationsByIdsCommand request, CancellationToken cancellationToken)
    {
        var notifications = await notificationRepository.GetUserNotificationsByIdsAsync(
            request.InitiatorId, request.Ids, cancellationToken, true);

        if (notifications.Count != request.Ids.Count)
        {
            throw new BadRequestException("Not all notifications were found");
        }
        
        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }
        
        await notificationRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}