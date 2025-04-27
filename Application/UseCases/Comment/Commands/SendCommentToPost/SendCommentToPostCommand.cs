using Application.Dtos.Comment;
using MediatR;

namespace Application.UseCases.Comment.Commands.SendCommentToPost;

public class SendCommentToPostCommand : IRequest<CommentReadDto>
{
    public Guid InitiatorId { get; init; }
    public Guid PostId { get; init; }
    public string Content { get; init; }
}