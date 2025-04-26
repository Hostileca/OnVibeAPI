using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface ICommentRepository
{
    Task AddAsync(Comment comment, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<IList<Comment>> GetPostCommentsAsync(Guid postId, PageInfo pageInfo, CommentIncludes includes, CancellationToken cancellationToken, bool trackChanges = false);
    Task<int> GetPostCommentsCountAsync(Guid postId, CancellationToken cancellationToken); 
}