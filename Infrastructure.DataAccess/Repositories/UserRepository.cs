using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class UserRepository(BaseDbContext context) : IUserRepository
{
    public async Task<User> RegisterUserAsync(User user, CancellationToken cancellationToken)
    {
        return (await context.Users.AddAsync(user, cancellationToken)).Entity;
    }

    public async Task<bool> IsEmailRegisteredAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.Users
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.Users
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IList<User>> GetUsersByIdsAsync(IList<Guid> ids, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.Users
            .TrackChanges(trackChanges)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}