using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using DataAccess.Contexts;
using DataAccess.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

internal class CommentRepository(BaseDbContext context) : ICommentRepository
{
    public async Task AddAsync(Comment comment, CancellationToken cancellationToken)
    {
        await context.Comments.AddAsync(comment, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<Comment>> GetPostCommentsAsync(Guid postId, PageInfo pageInfo, CommentIncludes includes, CancellationToken cancellationToken,
        bool trackChanges = false)
    {
        return await context.Comments
            .IncludeUser(includes.IncludeUser)
            .Where(x => x.PostId == postId)
            .Paged(pageInfo)
            .ToListAsync(cancellationToken);
    }
}