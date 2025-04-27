using Application.Dtos.Comment;
using Application.Dtos.Page;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Comment.Queries.GetPostComments;

public class GetPostCommentsQuery : RequestBase<PagedResponse<CommentReadDto>>
{
    public Guid PostId { get; init; }
    public PageData PageData { get; init; }
}