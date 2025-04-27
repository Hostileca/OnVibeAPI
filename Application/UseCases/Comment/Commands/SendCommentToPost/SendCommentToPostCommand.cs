using Application.Dtos.Comment;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Comment.Commands.SendCommentToPost;

public class SendCommentToPostCommand : RequestBase<CommentReadDto>
{
    public Guid PostId { get; init; }
    public string Content { get; init; }
}