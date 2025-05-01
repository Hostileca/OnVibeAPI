using Application.Dtos.User;

namespace Application.Dtos.Subscription;

public class SubscriptionReadDto
{
    public UserReadDto User { get; set; }
    public bool IsSubscribed { get; set; }
    public Guid SubscribedToId { get; set; }
    public Guid SubscribedById { get; set; }
}