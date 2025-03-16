using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IChatRepository
{
    Task<Chat?> GetChatByIdAsync(Guid chatId, ChatIncludes includes, CancellationToken cancellationToken, bool trackChanges = false);
    Task<IList<Chat>> GetUserChatsAsync(Guid userId, ChatIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken);
    Task AddChatAsync(Chat chat, CancellationToken cancellationToken);
    void RemoveChat(Chat chat);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}