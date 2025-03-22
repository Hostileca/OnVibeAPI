using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class MessageRepository(BaseDbContext context) : IMessageRepository
{
    public async Task<IList<Message>> GetMessagesByChatIdAsync(Guid chatId, PageInfo pageInfo, CancellationToken cancellationToken)
    {
        return await context.Messages
            .Where(x => x.ChatId == chatId)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Message message, CancellationToken cancellationToken)
    {
        await context.AddAsync(message, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}