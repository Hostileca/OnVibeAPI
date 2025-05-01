using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface ISubscriptionRepository
{
    Task<IList<Subscription>> GetSubscriptionsAsync(Guid subscribedById, PageInfo pageInfo, SubscriptionIncludes includes, CancellationToken cancellationToken);
    Task<IList<Subscription>> GetSubscribersAsync(Guid subscribedToId, PageInfo pageInfo, SubscriptionIncludes includes, CancellationToken cancellationToken);
    Task AddSubscriptionAsync(Subscription? subscription, CancellationToken cancellationToken);
    Task<bool> IsSubscriptionExistAsync(Guid subscribedToId, Guid subscribedById, CancellationToken cancellationToken);
    Task<Subscription?> GetSubscriptionAsync(Guid subscribedToId, Guid subscribedById, SubscriptionIncludes includes, CancellationToken cancellationToken);
    void RemoveSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<int> GetUserSubscribersCountAsync(Guid userId, CancellationToken cancellationToken);
    Task<int> GetUserSubscriptionsCountAsync(Guid userId, CancellationToken cancellationToken);
}