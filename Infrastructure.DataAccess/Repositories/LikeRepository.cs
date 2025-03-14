using Contracts.DataAccess.Interfaces;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class LikeRepository(BaseDbContext context) : ILikeRepository
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsLikeExistAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        return await context.Likes.AnyAsync(l => l.UserId == userId && l.PostId == postId, cancellationToken);
    }

    public async Task<Like?> GetLikeAsync(Guid postId, Guid userId, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.Likes
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId, cancellationToken);
    }

    public async Task AddLikeAsync(Like like, CancellationToken cancellationToken)
    {
        await context.Likes.AddAsync(like, cancellationToken);
    }

    public void RemoveLikeAsync(Like like)
    {
       context.Likes.Remove(like);
    }
}