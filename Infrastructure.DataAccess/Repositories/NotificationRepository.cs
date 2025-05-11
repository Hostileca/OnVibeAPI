using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class NotificationRepository(BaseDbContext context) : INotificationRepository
{
    public async Task AddAsync(NotificationBase notification, CancellationToken cancellationToken)
    {
        await context.Notifications.AddAsync(notification, cancellationToken);
    }

    public async Task<IList<NotificationBase>> GetUserNotificationsByIdsAsync(Guid userId, IList<Guid> ids, CancellationToken cancellationToken,
        bool trackChanges = false, bool excludeDelayed = true)
    {
        return await context.Notifications
            .ExcludeDelayed(excludeDelayed)
            .TrackChanges(trackChanges)
            .Where(x => x.UserId == userId && ids.Contains(x.Id))
            .ToListAsync(cancellationToken); 
    }

    public async Task<IList<NotificationBase>> GetUserNotificationsAsync(Guid userId, bool isRead, CancellationToken cancellationToken,
        bool trackChanges = false, bool excludeDelayed = true)
    {
        return await context.Notifications
            .ExcludeDelayed(excludeDelayed)
            .TrackChanges(trackChanges)
            .Where(x => x.UserId == userId && x.IsRead == isRead)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}