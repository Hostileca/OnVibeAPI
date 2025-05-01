using Application.Dtos.Page;
using Application.Dtos.Subscription;
using MediatR;

namespace Application.UseCases.Subscription.Queries.GetUserSubscribers;

public class GetUserSubscribersQuery : IRequest<PagedResponse<SubscriberReadDto>>
{
    public Guid UserId { get; init; }
    public PageData PageData { get; init; }
}