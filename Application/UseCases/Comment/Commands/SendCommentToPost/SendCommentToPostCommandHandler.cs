using Application.Dtos.Comment;
using Contracts.DataAccess.Interfaces;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Comment.Commands.SendCommentToPost;

public class SendCommentToPostCommandHandler(
    IPostRepository postRepository,
    ICommentRepository commentRepository) 
    : IRequestHandler<SendCommentToPostCommand, CommentReadDto>
{
    public async Task<CommentReadDto> Handle(SendCommentToPostCommand request, CancellationToken cancellationToken)
    {
        var post = await postRepository.GetPostByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Post), request.PostId.ToString());
        }

        var comment = request.Adapt<Domain.Entities.Comment>();
        
        await commentRepository.AddAsync(comment, cancellationToken);
        await commentRepository.SaveChangesAsync(cancellationToken);
        
        return comment.Adapt<CommentReadDto>();
    }
}