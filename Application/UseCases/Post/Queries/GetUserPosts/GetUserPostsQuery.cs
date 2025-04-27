using Application.Dtos.Page;
using Application.Dtos.Post;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Post.Queries.GetUserPosts;

public sealed class GetUserPostsQuery : RequestBase<PagedResponse<PostReadDto>>
{
    public Guid UserId { get; init; }
    public PageData PageData { get; init; }
}