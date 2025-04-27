using Application.Dtos.Comment;
using Application.Dtos.Page;
using MediatR;

namespace Application.UseCases.Comment.Queries.GetPostComments;

public class GetPostCommentsQuery : IRequest<PagedResponse<CommentReadDto>>
{
    public Guid PostId { get; init; }
    public PageData PageData { get; init; }
}