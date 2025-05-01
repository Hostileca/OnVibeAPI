using Application.Dtos.User;

namespace Application.Dtos.Subscription;

public class SubscriptionReadDto : SubReadDtoBase
{
    public UserReadDto SubscribedTo { get; set; }
}