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
    public async Task AddAsync(Message message, CancellationToken cancellationToken)
    {
        await context.AddAsync(message, cancellationToken);
    }

    public async Task<IList<Message>> GetAvailableToUserMessagesAsync(
        Guid chatId, 
        Guid userId, 
        MessageIncludes includes, 
        PageInfo pageInfo,
        CancellationToken cancellationToken, 
        bool trackChanges = false, 
        bool excludeDelayed = true)
    {
        var memberPeriod = await context.ChatMembers.GetChatMemberPeriod(cancellationToken);

        var query = context.Messages.GetAvailableToUserMessagesQuery(
                chatId, userId, excludeDelayed, memberPeriod.joinDate, memberPeriod.removeDate)
            .IncludeReactions(includes.IncludeReactions)
            .IncludeSender(includes.IncludeSender)
            .TrackChanges(trackChanges)
            .Paged(pageInfo);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Message?> GetAvailableToUserMessageAsync(Guid messageId, Guid chatId, Guid userId, MessageIncludes includes,
        CancellationToken cancellationToken, bool trackChanges = false, bool excludeDelayed = true)
    {
        var memberPeriod = await context.ChatMembers.GetChatMemberPeriod(cancellationToken);

        var query = context.Messages.GetAvailableToUserMessagesQuery(
                chatId, userId, excludeDelayed, memberPeriod.joinDate, memberPeriod.removeDate)
            .IncludeReactions(includes.IncludeReactions)
            .IncludeSender(includes.IncludeSender)
            .TrackChanges(trackChanges);
            
        return await query.FirstOrDefaultAsync(x => x.Id == messageId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}