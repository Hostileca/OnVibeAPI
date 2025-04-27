using Application.Dtos.Subscription;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Subscription.SubscribeToUser;

public sealed class SubscribeToUserCommand : RequestBase<SubscriptionReadDto>
{
    public Guid UserId { get; init; }
}