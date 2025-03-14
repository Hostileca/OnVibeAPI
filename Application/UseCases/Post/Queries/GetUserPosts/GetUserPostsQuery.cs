using Application.Dtos.Page;
using Application.Dtos.Post;
using MediatR;

namespace Application.UseCases.Post.Queries.GetUserPosts;

public sealed record GetUserPostsQuery(Guid UserId, PageData Page) : IRequest<PageResponse<PostReadDto>>;