using Application.Dtos.User;

namespace Application.Dtos.Subscription;

public class SubscriberReadDto : SubReadDtoBase
{
    public UserReadDto SubscribedBy { get; set; }
}