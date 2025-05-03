using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class ChatRepository(BaseDbContext context) : IChatRepository
{
    public Task<Chat?> GetChatByIdAsync(Guid chatId, ChatIncludes includes, CancellationToken cancellationToken,
        bool trackChanges = false)
    {
        return context.Chats
            .IncludeChatMembers(includes.IncludeChatMembers)
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == chatId, cancellationToken);
    }

    public async Task<IList<Chat>> GetUserChatsAsync(Guid userId, ChatIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken)
    {
        return await context.Chats
            .IncludeChatMembers(includes.IncludeChatMembers)
            .Where(x => x.Members.Any(cm => cm.UserId == userId))
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Guid>> GetAllUserChatsIds(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Chats
            .Where(chat => chat.Members.Any(cm => cm.UserId == userId))
            .Select(chat => chat.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Guid>> GetChatMembersIdsAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await context.ChatMembers
            .Where(x => x.ChatId == chatId)
            .Select(x => x.UserId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddChatAsync(Chat chat, CancellationToken cancellationToken)
    {
        await context.Chats.AddAsync(chat, cancellationToken);
    }

    public void RemoveChat(Chat chat)
    {
        context.Chats.Remove(chat);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}