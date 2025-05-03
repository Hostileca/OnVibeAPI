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
}