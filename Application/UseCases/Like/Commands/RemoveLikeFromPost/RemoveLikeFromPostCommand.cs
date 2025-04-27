using Application.Dtos.Like;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Like.Commands.RemoveLikeFromPost;

public sealed class RemoveLikeFromPostCommand : RequestBase<LikeReadDto>
{
    public Guid PostId { get; init; }
}