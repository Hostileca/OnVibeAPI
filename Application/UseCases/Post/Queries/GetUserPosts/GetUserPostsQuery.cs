using Application.Dtos.Page;
using Application.Dtos.Post;
using MediatR;

namespace Application.UseCases.Post.Queries.GetUserPosts;

public sealed class GetUserPostsQuery : IRequest<PagedResponse<PostReadDto>>
{
    public Guid UserId { get; init; }
    public PageData PageData { get; init; }
}