using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class ChatQueryableExtensions
{
    public static IQueryable<Chat> IncludeChatMembers(
        this IQueryable<Chat> chats,
        bool includeUsers)
    {
        if (!includeUsers)
        {
            return chats;
        }

        return chats.Include(chat => chat.ChatsMembers);
    }
}