using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class ChatMembersRepository(BaseDbContext context) : IChatMembersRepository
{
    public async Task AddChatMemberAsync(ChatMember chatMember, CancellationToken cancellationToken)
    {
        await context.ChatMembers.AddAsync(chatMember, cancellationToken);
    }

    public async Task<IList<ChatMember>> GetChatMembersAsync(Guid chatId, ChatMemberIncludes includes, CancellationToken cancellationToken, bool excludeRemoved = true)
    {
        return await context.ChatMembers
            .IncludeUser(includes.IncludeUser)
            .ExcludeRemoved(excludeRemoved)
            .Where(x => x.ChatId == chatId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ChatMember?> GetChatMemberAsync(Guid userId, Guid chatId, ChatMemberIncludes includes, CancellationToken cancellationToken, bool trackChanges = false, bool excludeRemoved = true)
    {
        return await context.ChatMembers
            .IncludeUser(includes.IncludeUser)
            .TrackChanges(trackChanges)
            .ExcludeRemoved(excludeRemoved)
            .FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == userId, cancellationToken);
    }
}