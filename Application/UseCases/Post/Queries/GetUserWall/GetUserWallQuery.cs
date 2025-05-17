using Application.Dtos.Page;
using Application.Dtos.Post;
using MediatR;

namespace Application.UseCases.Post.Queries.GetUserWall;

public class GetUserWallQuery : IRequest<PagedResponse<PostReadDto>>
{
    public Guid UserId { get; set; }
    public PageData PageData { get; set; }
}