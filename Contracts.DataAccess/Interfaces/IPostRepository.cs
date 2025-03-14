using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IPostRepository
{
    public Task AddPostAsync(Post post, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<IList<Post>> GetPostsByUserIdAsync(Guid id, PostIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken);
    public Task<int> GetPostLikesCountAsync(Guid postId, CancellationToken cancellationToken);
    public Task<int> GetPostCommentsCountAsync(Guid postId, CancellationToken cancellationToken);
    public Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false);
}