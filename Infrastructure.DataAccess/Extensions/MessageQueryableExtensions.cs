using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class MessageQueryableExtensions
{
    public static IQueryable<Message> ExcludeDelayed(
        this IQueryable<Message> messages,
        bool excludeDelayed)
    {
        if (!excludeDelayed)
        {
            return messages;
        }

        return messages.Where(message => message.Date < DateTime.UtcNow);
    }

    public static IQueryable<Message> GetAvailableToUserMessagesQuery(
        this IQueryable<Message> messages,
        Guid chatId, 
        Guid userId, 
        bool excludeDelayed,
        DateTime joinDate,
        DateTime? removeDate)
    {
        return messages
            .OrderByDescending(m => m.Date)
            .Where(message => 
                message.ChatId == chatId &&
                message.Chat.Members.Any(member => member.UserId == userId))
            .FilterByDate(joinDate, removeDate)
            .ExcludeDelayed(excludeDelayed);
    }
    
    public static IQueryable<Message> Includes(
        this IQueryable<Message> messages,
        MessageIncludes includes)
    {
        return messages
            .IncludeReactions(includes.IncludeReactions)
            .IncludeSender(includes.IncludeSender)
            .IncludeNotifications(includes.IncludeNotifications);
    }
    
    private static IQueryable<Message> IncludeReactions(
        this IQueryable<Message> messages,
        bool includeReactions)
    {
        if (!includeReactions)
        {
            return messages;
        }

        return messages.Include(message => message.Reactions);
    }
    
    private static IQueryable<Message> IncludeSender(
        this IQueryable<Message> messages,
        bool includeSender)
    {
        if (!includeSender)
        {
            return messages;
        }

        return messages.Include(message => message.Sender);
    }
    
    private static IQueryable<Message> IncludeNotifications(
        this IQueryable<Message> messages,
        bool includeNotifications)
    {
        if (!includeNotifications)
        {
            return messages;
        }

        return messages.Include(message => message.Notifications);
    }
    
    private static IQueryable<Message> FilterByDate(
        this IQueryable<Message> messages,
        DateTime startDate,
        DateTime? endDate)
    {
        messages = messages.Where(message => message.Date >= startDate);
        if (endDate.HasValue)
        {
            messages = messages.Where(message => message.Date <= endDate.Value);
        }

        return messages;
    }
}