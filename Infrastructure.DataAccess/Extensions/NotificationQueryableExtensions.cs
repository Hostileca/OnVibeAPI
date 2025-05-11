using Domain.Entities.Notifications;

namespace DataAccess.Extensions;

public static class NotificationQueryableExtensions
{
    public static IQueryable<NotificationBase> ExcludeDelayed(
        this IQueryable<NotificationBase> notifications,
        bool excludeDelayed)
    {
        if (!excludeDelayed)
        {
            return notifications;
        }

        return notifications.Where(message => message.Date < DateTime.UtcNow);
    }
}