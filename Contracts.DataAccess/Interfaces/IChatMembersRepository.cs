using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IChatMembersRepository
{
    Task<IList<ChatMember>> GetChatMembersAsync(Guid chatId, CancellationToken cancellationToken);

    Task<ChatMember?> GetChatMemberAsync(Guid userId, Guid chatId, ChatMemberIncludes includes, CancellationToken cancellationToken, bool trackChanges = false);
    void Remove(ChatMember chatMember);
}