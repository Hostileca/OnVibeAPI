﻿using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.Commands.UpsertSubscription;

public sealed class UpsertSubscriptionCommand : IRequest<SubscriptionReadDto>
{
    public Guid InitiatorId { get; init; }
    public Guid UserId { get; init; }
}