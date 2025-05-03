using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class MessageQueryableExtensions
{
    public static IQueryable<Message> IncludeReactions(
        this IQueryable<Message> messages,
        bool includeReactions)
    {
        if (!includeReactions)
        {
            return messages;
        }

        return messages.Include(message => message.Reactions);
    }
    
    public static IQueryable<Message> IncludeSender(
        this IQueryable<Message> messages,
        bool includeSender)
    {
        if (!includeSender)
        {
            return messages;
        }

        return messages.Include(message => message.Sender);
    }

    public static IQueryable<Message> ExcludeDelayed(
        this IQueryable<Message> messages,
        bool includeDelayed)
    {
        if (!includeDelayed)
        {
            return messages;
        }

        return messages.Where(message => message.Date < DateTime.UtcNow);
    }
    
    public static IQueryable<Message> FilterByDate(
        this IQueryable<Message> messages,
        DateTime startDate,
        DateTime? endDate)
    {
        messages = messages.Where(message => message.Date > startDate);
        if (endDate.HasValue)
        {
            messages = messages.Where(message => message.Date > endDate.Value);
        }

        return messages;
    }
}