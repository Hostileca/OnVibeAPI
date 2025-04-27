using MediatR;

namespace Application.UseCases.User.Queries.GetUserAvatar;

public sealed class GetUserAvatarQuery : IRequest<byte[]>
{
    public Guid UserId { get; init; }
}