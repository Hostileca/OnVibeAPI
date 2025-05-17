using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories;

internal class PostRepository(BaseDbContext context) : IPostRepository
{
    public async Task AddPostAsync(Post post, CancellationToken cancellationToken)
    {
        await context.Posts.AddAsync(post, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<Post>> GetPostsByUserIdAsync(Guid userId, PostIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken)
    {
        return await context.Posts
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Date)
            .IncludeUser(includes.IncludeUser)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task<IList<Post>> GetWallByUserIdAsync(Guid userId, PostIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken)
    {
        var userSubscriptions = context.Subscriptions
            .Where(sub => sub.SubscribedById == userId)
            .Select(sub => sub.SubscribedToId);

        return await context.Posts
            .Where(post => userSubscriptions.Contains(post.UserId))
            .OrderByDescending(post => post.Date)
            .IncludeUser(includes.IncludeUser)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public async Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.Posts
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<int> GetUserPostsCountAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await context.Posts
            .Where(post => post.UserId == userId)
            .CountAsync(cancellationToken);
    }
}