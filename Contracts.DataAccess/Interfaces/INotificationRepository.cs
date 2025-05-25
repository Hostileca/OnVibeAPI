using Domain.Entities.Notifications;

namespace Contracts.DataAccess.Interfaces;

public interface INotificationRepository
{
    Task AddAsync(NotificationBase notification, CancellationToken cancellationToken);
    Task<IList<NotificationBase>> GetUserNotificationsByIdsAsync(Guid userId, IList<Guid> ids, CancellationToken cancellationToken, bool trackChanges = false, bool excludeDelayed = true);
    Task<bool> IsMessageReadByUserAsync(Guid userId, Guid messageId, CancellationToken cancellationToken);
    Task<int> GetUnreadMessagesInChatCountAsync(Guid chatId, Guid userId, CancellationToken cancellationToken);
    Task<IList<NotificationBase>> GetUserNotificationsAsync(Guid userId, bool isRead, CancellationToken cancellationToken, bool trackChanges = false, bool excludeDelayed = true);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}