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
}