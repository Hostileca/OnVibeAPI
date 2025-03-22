using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IChatMembersRepository
{
    Task<IList<ChatMember>> GetChatMembersAsync(Guid chatId, CancellationToken cancellationToken);
}