using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface ILikeRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<bool> IsLikeExistAsync(Guid postId, Guid userId, CancellationToken cancellationToken);
    Task<Like?> GetLikeAsync(Guid postId, Guid userId, CancellationToken cancellationToken, bool trackChanges = false);
    Task AddLikeAsync(Like like, CancellationToken cancellationToken);
    void RemoveLikeAsync(Like like);
}