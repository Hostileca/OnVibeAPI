using Contracts.DataAccess.Models;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IMessageRepository
{
    Task<IList<Message>> GetMessagesByChatIdAsync(Guid chatId, PageInfo pageInfo, CancellationToken cancellationToken);
    Task AddAsync(Message message, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}