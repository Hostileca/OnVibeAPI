using Application.Dtos.Comment;
using Application.Dtos.Page;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models;
using Contracts.DataAccess.Models.Include;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Comment.Queries.GetPostComments;

public class GetPostCommentsQueryHandler(
    IPostRepository postRepository,
    ICommentRepository commentRepository)
    : IRequestHandler<GetPostCommentsQuery, PagedResponse<CommentReadDto>>
{
    public async Task<PagedResponse<CommentReadDto>> Handle(GetPostCommentsQuery request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Post), request.PostId.ToString());
        }
        
        var comments = await commentRepository.GetPostCommentsAsync(
            request.PostId,
            request.PageData.Adapt<PageInfo>(),
            new CommentIncludes { IncludeUser = true },
            cancellationToken);
        
        var commentsDto = comments.Adapt<List<CommentReadDto>>();
        
        return new PagedResponse<CommentReadDto>(
            commentsDto, 
            request.PageData.PageNumber, 
            request.PageData.PageSize);
    }
}