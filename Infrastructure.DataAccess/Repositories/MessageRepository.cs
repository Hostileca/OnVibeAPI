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
        var chat = await context.Chats
            .AsNoTracking()
            .Where(chat => chat.Id == chatId)
            .Select(chat => new { chat.Id })
            .FirstOrDefaultAsync(cancellationToken);

        if (chat == null)
        {
            return new List<Message>();
        }

        var chatMember = await context.ChatMembers
            .AsNoTracking()
            .FirstOrDefaultAsync(cm => cm.UserId == userId && cm.ChatId == chat.Id, cancellationToken);

        if (chatMember == null)
        {
            return new List<Message>();
        }

        var query = context.Messages
            .OrderByDescending(m => m.Date)
            .Where(m => m.ChatId == chat.Id)
            .FilterByDate(chatMember.JoinDate, chatMember.RemoveDate)
            .ExcludeDelayed(excludeDelayed)
            .IncludeReactions(includes.IncludeReactions)
            .IncludeSender(includes.IncludeSender)
            .TrackChanges(trackChanges)
            .Paged(pageInfo);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Message?> GetAvailableToUserMessageAsync(Guid messageId, Guid userId, MessageIncludes includes,
        CancellationToken cancellationToken, bool trackChanges = false, bool excludeDelayed = true)
    {
        var chatMember = await context.ChatMembers.AsNoTracking().FirstOrDefaultAsync(chatMember =>
            chatMember.UserId == userId && chatMember.Chat.Messages.Any(message => message.Id == messageId), cancellationToken);
        if (chatMember is null)
        {
            return null;
        }
        return await context.Messages
            .FilterByDate(chatMember.JoinDate, chatMember.RemoveDate)
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