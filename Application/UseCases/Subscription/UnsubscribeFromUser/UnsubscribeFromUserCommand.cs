using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.UnsubscribeFromUser;

public sealed class UnsubscribeFromUserCommand : IRequest<SubscriptionReadDto>
{
    public Guid InitiatorId { get; init; }
    public Guid UserId { get; init; }
}