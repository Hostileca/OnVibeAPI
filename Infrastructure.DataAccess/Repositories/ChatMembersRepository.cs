using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class ChatMembersRepository(BaseDbContext context) : IChatMembersRepository
{
    public async Task<IList<ChatMember>> GetChatMembersAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await context.ChatMembers.Where(x => x.ChatId == chatId).ToListAsync(cancellationToken);
    }

    public async Task<ChatMember?> GetChatMemberAsync(Guid userId, Guid chatId, ChatMemberIncludes includes, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.ChatMembers
            .IncludeUser(includes.IncludeUser)
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.ChatId == chatId && x.UserId == userId, cancellationToken);
    }

    public void Remove(ChatMember chatMember)
    {
        context.ChatMembers.Remove(chatMember);
    }
}