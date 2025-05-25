using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class ChatMemberQueryableExtensions
{
    public static IQueryable<ChatMember> IncludeUser(
        this IQueryable<ChatMember> chatMembers,
        bool includeUser)
    {
        if (!includeUser)
        {
            return chatMembers;
        }

        return chatMembers.Include(chat => chat.User);
    }
    
    public static IQueryable<ChatMember> ExcludeRemoved(
        this IQueryable<ChatMember> chatMember,
        bool excludeRemoved)
    {
        if (!excludeRemoved)
        {
            return chatMember;
        }

        return chatMember.Where(message => !message.RemoveDate.HasValue);
    }

    public static async Task<(DateTime joinDate, DateTime? removeDate)> GetChatMemberPeriod(
        this IQueryable<ChatMember> chatMembers, 
        CancellationToken cancellationToken)
    {
        return await chatMembers
            .AsNoTracking()
            .Select(cm => new ValueTuple<DateTime, DateTime?>(cm.JoinDate, cm.RemoveDate))
            .FirstOrDefaultAsync(cancellationToken);
    }
}