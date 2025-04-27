using Application.Dtos.Notification;
using MediatR;

namespace Application.UseCases.Notification.Queries.GetUserNotifications;

public class GetUserNotificationsQuery : IRequest<IList<NotificationBaseReadDto>>
{
    public bool IsRead { get; init; }
    public Guid InitiatorId { get; init; }
}