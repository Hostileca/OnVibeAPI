using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class ChatMembersRepository(BaseDbContext context) : IChatMembersRepository
{
    public async Task<IList<ChatMember>> GetChatMembersAsync(Guid chatId, CancellationToken cancellationToken)
    {
        return await context.ChatMembers.Where(x => x.ChatId == chatId).ToListAsync(cancellationToken);
    }
}