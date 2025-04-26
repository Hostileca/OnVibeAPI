using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;

namespace Contracts.DataAccess.Interfaces;

public interface IPostRepository
{ 
    Task AddPostAsync(Post post, CancellationToken cancellationToken); 
    Task SaveChangesAsync(CancellationToken cancellationToken); 
    Task<IList<Post>> GetPostsByUserIdAsync(Guid id, PostIncludes includes, PageInfo pageInfo, CancellationToken cancellationToken); 
    Task<Post?> GetPostByIdAsync(Guid id, CancellationToken cancellationToken, bool trackChanges = false);
    Task<int> GetUserPostsCountAsync(Guid userId, CancellationToken cancellationToken);
}