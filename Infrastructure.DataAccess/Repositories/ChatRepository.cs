using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ChatRepository(BaseDbContext context) : IChatRepository
{
    public Task<Chat?> GetChatByIdAsync(Guid chatId, ChatIncludes includes, CancellationToken cancellationToken,
        bool trackChanges = false)
    {
        return context.Chats
            .IncludeChatMembers(includes.IncludeChatMembers)
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == chatId, cancellationToken);
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