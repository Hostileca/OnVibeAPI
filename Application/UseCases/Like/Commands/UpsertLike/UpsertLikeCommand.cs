using Application.Dtos.Like;
using MediatR;

namespace Application.UseCases.Like.Commands.UpsertLike;

public sealed class UpsertLikeCommand : IRequest<LikeReadDto>
{
    public Guid PostId { get; init; }
    public Guid InitiatorId { get; init; }
}