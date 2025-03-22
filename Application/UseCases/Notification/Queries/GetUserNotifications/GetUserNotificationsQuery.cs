using Application.Dtos.Notification;
using MediatR;

namespace Application.UseCases.Notification.Queries.GetUserNotifications;

public record GetUserNotificationsQuery(Guid UserId, bool IsRead) : IRequest<IList<NotificationBaseReadDto>>;