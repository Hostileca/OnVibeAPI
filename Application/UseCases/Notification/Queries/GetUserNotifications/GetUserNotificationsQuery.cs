using Application.Dtos.Notification;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Notification.Queries.GetUserNotifications;

public class GetUserNotificationsQuery : RequestBase<IList<NotificationBaseReadDto>>
{
    public bool IsRead { get; init; }
}