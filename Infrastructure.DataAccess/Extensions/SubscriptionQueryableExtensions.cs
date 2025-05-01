using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class SubscriptionQueryableExtensions
{
    public static IQueryable<Subscription> IncludeSubscribedTo(
        this IQueryable<Subscription> subscriptions,
        bool includeSubscribedTo)
    {
        if (!includeSubscribedTo)
        {
            return subscriptions;
        }

        return subscriptions.Include(subscription => subscription.SubscribedTo);
    }
    
    public static IQueryable<Subscription> IncludeSubscribedBy(
        this IQueryable<Subscription> subscriptions,
        bool includeSubscribedBy)
    {
        if (!includeSubscribedBy)
        {
            return subscriptions;
        }

        return subscriptions.Include(subscription => subscription.SubscribedBy);
    }
}