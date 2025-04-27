using Application.Dtos.Like;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Like.Commands.AddLikeToPost;

public sealed class AddLikeToPostCommand : RequestBase<LikeReadDto>
{
    public Guid PostId { get; init; }
}