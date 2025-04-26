using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class SubscriptionRepository(BaseDbContext context) : ISubscriptionRepository
{
    public async Task AddSubscriptionAsync(Subscription? subscription, CancellationToken cancellationToken)
    {
        await context.Subscriptions.AddAsync(subscription, cancellationToken);
    }

    public async Task<bool> IsSubscriptionExistAsync(Guid subscribedToId, Guid subscribedById, CancellationToken cancellationToken)
    {
        return await context.Subscriptions.AnyAsync(
            s => s.SubscribedById == subscribedById && s.SubscribedToId == subscribedToId, cancellationToken);
    }

    public async Task<Subscription?> GetSubscriptionAsync(Guid subscribedToId, Guid subscribedById, CancellationToken cancellationToken)
    {
        return await context.Subscriptions.FirstOrDefaultAsync(
            s => s.SubscribedById == subscribedById && s.SubscribedToId == subscribedToId, cancellationToken);
    }

    public void RemoveSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken)
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