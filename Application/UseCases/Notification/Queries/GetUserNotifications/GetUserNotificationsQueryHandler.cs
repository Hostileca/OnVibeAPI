using Application.Dtos.Notification;
using Contracts.DataAccess.Interfaces;
using Mapster;
using MediatR;

namespace Application.UseCases.Notification.Queries.GetUserNotifications;

public class GetUserNotificationsQueryHandler(
    INotificationRepository notificationRepository)
    : IRequestHandler<GetUserNotificationsQuery, IList<NotificationBaseReadDto>>
{
    public async Task<IList<NotificationBaseReadDto>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await notificationRepository.GetUserNotificationsAsync(
            request.InitiatorId, request.IsRead, cancellationToken);
        
        return notifications.Adapt<IList<NotificationBaseReadDto>>();
    }
}