using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IChatRepository
{
    Task<Chat?> GetChatByIdAsync(Guid chatId, ChatIncludes includes, CancellationToken cancellationToken, bool trackChanges = false);
    Task AddChatAsync(Chat chat, CancellationToken cancellationToken);
    void RemoveChat(Chat chat);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}