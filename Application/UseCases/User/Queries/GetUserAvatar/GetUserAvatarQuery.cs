using MediatR;

namespace Application.UseCases.User.Queries.GetUserAvatar;

public sealed record GetUserAvatarQuery(Guid UserId) : IRequest<byte[]>;