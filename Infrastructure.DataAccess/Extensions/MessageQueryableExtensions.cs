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
}