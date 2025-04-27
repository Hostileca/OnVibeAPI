using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.User.Queries.GetUserAvatar;

public sealed class GetUserAvatarQuery : RequestBase<byte[]>
{
    public Guid UserId { get; init; }
}