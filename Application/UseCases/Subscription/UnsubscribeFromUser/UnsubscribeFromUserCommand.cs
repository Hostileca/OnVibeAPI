using Application.Dtos.Subscription;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Subscription.UnsubscribeFromUser;

public sealed class UnsubscribeFromUserCommand : RequestBase<SubscriptionReadDto>
{
    public Guid UserId { get; init; }
}