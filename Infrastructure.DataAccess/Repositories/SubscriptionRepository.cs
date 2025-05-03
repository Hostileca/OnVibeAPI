using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class SubscriptionRepository(BaseDbContext context) : ISubscriptionRepository
{
    public async Task<IList<Subscription>> GetSubscriptionsAsync(Guid subscribedById, PageInfo pageInfo, SubscriptionIncludes includes, CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .IncludeSubscribedBy(includes.IncludeSubscribedBy)
            .IncludeSubscribedTo(includes.IncludeSubscribedTo)
            .Where(subscription => subscription.SubscribedById == subscribedById)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Subscription>> GetSubscribersAsync(Guid subscribedToId, PageInfo pageInfo, SubscriptionIncludes includes, CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .IncludeSubscribedBy(includes.IncludeSubscribedBy)
            .IncludeSubscribedTo(includes.IncludeSubscribedTo)
            .Where(subscription => subscription.SubscribedToId == subscribedToId)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task AddSubscriptionAsync(Subscription? subscription, CancellationToken cancellationToken)
    {
        await context.Subscriptions.AddAsync(subscription, cancellationToken);
    }

    public async Task<bool> IsSubscriptionExistAsync(Guid subscribedToId, Guid subscribedById, CancellationToken cancellationToken)
    {
        return await context.Subscriptions.AnyAsync(
            s => s.SubscribedById == subscribedById && s.SubscribedToId == subscribedToId, cancellationToken);
    }

    public async Task<Subscription?> GetSubscriptionAsync(Guid subscribedToId, Guid subscribedById, SubscriptionIncludes includes, CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .IncludeSubscribedBy(includes.IncludeSubscribedBy)
            .IncludeSubscribedTo(includes.IncludeSubscribedTo)
            .FirstOrDefaultAsync(s => s.SubscribedById == subscribedById && s.SubscribedToId == subscribedToId, cancellationToken);
    }

    public void RemoveSubscription(Subscription subscription)
    {
        context.Subscriptions.Remove(subscription);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetUserSubscribersCountAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .Where(post => post.SubscribedToId == userId)
            .CountAsync(cancellationToken);
    }

    public async Task<int> GetUserSubscriptionsCountAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .Where(post => post.SubscribedById == userId)
            .CountAsync(cancellationToken);
    }
}