using Application.Dtos.Like;
using MediatR;

namespace Application.UseCases.Like.Commands.AddLikeToPost;

public sealed class AddLikeToPostCommand : IRequest<LikeReadDto>
{
    public Guid PostId { get; init; }
    public Guid InitiatorId { get; init; }
}