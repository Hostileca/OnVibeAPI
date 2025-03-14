using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.UnsubscribeFromUser;

public sealed record UnsubscribeFromUserCommand(Guid InitiatorId, Guid UserId) : IRequest<SubscriptionReadDto>;