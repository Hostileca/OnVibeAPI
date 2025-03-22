using Domain.Entities.Notifications;

namespace Contracts.DataAccess.Interfaces;

public interface INotificationRepository
{
    Task AddAsync(NotificationBase notification, CancellationToken cancellationToken);
    Task<IList<NotificationBase>> GetUserNotificationsByIdsAsync(Guid userId, IList<Guid> ids, CancellationToken cancellationToken, bool trackChanges = false);
    Task<IList<NotificationBase>> GetUserNotificationsAsync(Guid userId, bool isRead, CancellationToken cancellationToken, bool trackChanges = false);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}