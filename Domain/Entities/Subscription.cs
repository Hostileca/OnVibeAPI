namespace Domain.Entities;

public class Subscription
{
    public User SubscribedTo { get; set; }
    public Guid SubscribedToId { get; set; }
    public User SubscribedBy { get; set; }
    public Guid SubscribedById { get; set; }
}