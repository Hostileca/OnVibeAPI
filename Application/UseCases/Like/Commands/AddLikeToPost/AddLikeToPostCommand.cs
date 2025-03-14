using Application.Dtos.Like;
using MediatR;

namespace Application.UseCases.Like.Commands.AddLikeToPost;

public sealed record AddLikeToPostCommand(Guid PostId, Guid UserId) : IRequest<LikeReadDto>;