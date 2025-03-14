using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface ISubscriptionRepository
{
    Task AddSubscriptionAsync(Subscription? subscription, CancellationToken cancellationToken);
    Task<bool> IsSubscriptionExistAsync(Guid subscribedToId, Guid subscribedById, CancellationToken cancellationToken);
    Task<Subscription?> GetSubscriptionAsync(Guid subscribedToId, Guid subscribedById, CancellationToken cancellationToken);
    void RemoveSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}