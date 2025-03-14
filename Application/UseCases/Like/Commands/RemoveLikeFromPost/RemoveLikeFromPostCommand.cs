using Application.Dtos.Like;
using MediatR;

namespace Application.UseCases.Like.Commands.RemoveLikeFromPost;

public sealed record RemoveLikeFromPostCommand(Guid PostId, Guid UserId) : IRequest<LikeReadDto>;