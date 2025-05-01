using Application.Dtos.Page;
using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.Queries.GetUserSubscribers;

public class GetUserSubscribersQuery : IRequest<PagedResponse<SubscriptionReadDto>>
{
    public Guid UserId { get; init; }
    public PageData PageData { get; init; }
}