using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IChatMembersRepository
{
    Task AddChatMemberAsync(ChatMember chatMember, CancellationToken cancellationToken);
    Task<IList<ChatMember>> GetChatMembersAsync(Guid chatId, ChatMemberIncludes includes, CancellationToken cancellationToken, bool excludeRemoved = true);
    Task<ChatMember?> GetChatMemberAsync(Guid userId, Guid chatId, ChatMemberIncludes includes, CancellationToken cancellationToken, bool trackChanges = false, bool excludeRemoved = true);
}