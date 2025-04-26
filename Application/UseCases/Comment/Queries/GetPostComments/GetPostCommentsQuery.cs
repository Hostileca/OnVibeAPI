using Application.Dtos.Comment;
using Application.Dtos.Page;
using MediatR;

namespace Application.UseCases.Comment.Queries.GetPostComments;

public record GetPostCommentsQuery(Guid PostId, PageData PageData) : IRequest<PagedResponse<CommentReadDto>>;