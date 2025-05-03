using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IMessageRepository
{
    Task<IList<Message>> GetMessagesByChatIdAsync(Guid chatId, PageInfo pageInfo, MessageIncludes includes, CancellationToken cancellationToken, bool excludeDelayed = true);
    Task AddAsync(Message message, CancellationToken cancellationToken);
    Task<IList<Message>> GetAvailableToUserMessagesAsync(
        Guid messageId, 
        Guid userId, 
        MessageIncludes includes, 
        PageInfo pageInfo,
        CancellationToken cancellationToken,
        bool trackChanges = false, 
        bool excludeDelayed = true);
    Task<Message?> GetAvailableToUserMessageAsync(
        Guid messageId, 
        Guid userId, 
        MessageIncludes includes, 
        CancellationToken cancellationToken,
        bool trackChanges = false, 
        bool excludeDelayed = true);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}