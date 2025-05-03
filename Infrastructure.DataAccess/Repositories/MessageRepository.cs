using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class MessageRepository(BaseDbContext context) : IMessageRepository
{
    public async Task<IList<Message>> GetMessagesByChatIdAsync(Guid chatId, PageInfo pageInfo, MessageIncludes includes, CancellationToken cancellationToken, bool excludeDelayed = true)
    {
        return await context.Messages
            .Where(x => x.ChatId == chatId)
            .ExcludeDelayed(excludeDelayed)
            .OrderByDescending(x => x.Date)
            .IncludeReactions(includes.IncludeReactions)
            .IncludeSender(includes.IncludeSender)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Message message, CancellationToken cancellationToken)
    {
        await context.AddAsync(message, cancellationToken);
    }

    public async Task<Message?> GetAvailableToUserMessageAsync(Guid messageId, Guid userId, MessageIncludes includes,
        CancellationToken cancellationToken, bool trackChanges = false, bool excludeDelayed = true)
    {
        return await context.Messages
            .ExcludeDelayed(excludeDelayed)
            .IncludeReactions(includes.IncludeReactions)
            .IncludeSender(includes.IncludeSender)
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == messageId && x.Chat.Members.Any(cm => cm.UserId == userId), cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}