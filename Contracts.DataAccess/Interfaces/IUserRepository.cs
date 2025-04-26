using Contracts.DataAccess.Models;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IUserRepository
{
    Task<IList<User>> SearchUsersAsync(UsersSearchCriteria searchCriteria, PageInfo pageInfo, CancellationToken cancellationToken);
    Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken); 
    Task<bool> IsEmailRegisteredAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken, bool trackChanges = false);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false);
    Task<IList<User>> GetUsersByIdsAsync(IList<Guid> ids, CancellationToken cancellationToken, bool trackChanges = false);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}