using Application.Dtos.Like;
using MediatR;

namespace Application.UseCases.Like.Commands.RemoveLikeFromPost;

public sealed class RemoveLikeFromPostCommand : IRequest<LikeReadDto>
{
    public Guid PostId { get; init; }
    public Guid InitiatorId { get; init; }
}