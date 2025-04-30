using Application.Dtos.User;
using Application.Services.Interfaces;
using Contracts.DataAccess.Interfaces;

namespace Application.Services.Implementations.ExtraLoaders;

public class UserReadDtoExtraLoader(
    IPostRepository postRepository,
    ISubscriptionRepository subscriptionRepository,
    IUserContext userContext) 
    : ExtraLoaderBase<UserReadDto>
{
    public override async Task LoadExtraInformationAsync(UserReadDto dto, CancellationToken cancellationToken = default)
    {
        dto.PostsCount = await postRepository.GetUserPostsCountAsync(dto.Id, cancellationToken);
        dto.SubscribersCount = await subscriptionRepository.GetUserSubscribersCountAsync(dto.Id, cancellationToken);
        dto.SubscriptionsCount = await subscriptionRepository.GetUserSubscriptionsCountAsync(dto.Id, cancellationToken);
        dto.IsSubscribed = await subscriptionRepository.IsSubscriptionExistAsync(dto.Id, userContext.InitiatorId, cancellationToken);
    }
}