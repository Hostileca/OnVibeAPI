using Application.Dtos.User;
using Contracts.DataAccess.Interfaces;

namespace Application.Dtos.ExtraLoaders;

public class UserReadDtoExtraLoader(
    IPostRepository postRepository,
    ISubscriptionRepository subscriptionRepository) 
    : ExtraLoaderBase<UserReadDto>
{
    public override async Task LoadExtraInformationAsync(UserReadDto dto, CancellationToken cancellationToken)
    {
        dto.PostsCount = await postRepository.GetUserPostsCountAsync(dto.Id, cancellationToken);
        dto.SubscribersCount = await subscriptionRepository.GetUserSubscribersCountAsync(dto.Id, cancellationToken);
        dto.SubscriptionsCount = await subscriptionRepository.GetUserSubscriptionsCountAsync(dto.Id, cancellationToken); 
    }
}