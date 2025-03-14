using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.SubscribeToUser;

public sealed record SubscribeToUserCommand(Guid InitiatorId, Guid UserId) : IRequest<SubscriptionReadDto>;