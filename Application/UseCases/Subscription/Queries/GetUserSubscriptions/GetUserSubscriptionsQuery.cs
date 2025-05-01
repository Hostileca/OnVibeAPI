using Application.Dtos.Page;
using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.Queries.GetUserSubscriptions;

public class GetUserSubscriptionsQuery : IRequest<PagedResponse<SubscriptionReadDto>>
{
    public Guid UserId { get; init; }
    public PageData PageData { get; init; }
}