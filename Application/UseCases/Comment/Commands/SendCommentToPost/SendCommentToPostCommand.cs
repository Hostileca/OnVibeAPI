using Application.Dtos.Comment;
using MediatR;

namespace Application.UseCases.Comment.Commands.SendCommentToPost;

public class SendCommentToPostCommand : IRequest<CommentReadDto>
{
    public Guid PostId { get; set; }
    public Guid InitiatorId { get; set; }
    public string Content { get; set; }
}