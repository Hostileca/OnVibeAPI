using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IMessageRepository
{
    Task<IList<Message>> GetMessagesByChatIdAsync(Guid chatId, PageInfo pageInfo, MessageIncludes includes, CancellationToken cancellationToken);
    Task AddAsync(Message message, CancellationToken cancellationToken);
    Task<Message?> GetAvailableToUserMessageAsync(Guid messageId, Guid userId, MessageIncludes includes, CancellationToken cancellationToken, bool trackChanges = false);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}