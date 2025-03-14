using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Repositories;

public class PostRepository(BaseDbContext context) : IPostRepository
{
    public async Task AddPostAsync(Post post, CancellationToken cancellationToken)
    {
        await context.Posts.AddAsync(post, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<Post>> GetPostsByUserIdAsync(Guid id, PostIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken)
    {
        return await context.Posts
            .Where(x => x.UserId == id)
            .IncludeUser(includes.IncludeUser)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }

    public Task<int> GetPostLikesCountAsync(Guid postId, CancellationToken cancellationToken)
    {
        return context.Likes.CountAsync(x => x.PostId == postId, cancellationToken);
    }

    public Task<int> GetPostCommentsCountAsync(Guid postId, CancellationToken cancellationToken)
    {
        return context.Comments.CountAsync(x => x.PostId == postId, cancellationToken);
    }

    public async Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false)
    {
        return await context.Posts
            .TrackChanges(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}