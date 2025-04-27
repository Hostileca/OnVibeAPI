using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.SubscribeToUser;

public sealed class SubscribeToUserCommand : IRequest<SubscriptionReadDto>
{
    public Guid InitiatorId { get; init; }
    public Guid UserId { get; init; }
}