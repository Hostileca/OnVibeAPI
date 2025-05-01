namespace Application.Dtos.Subscription;

public abstract class SubReadDtoBase
{
    public bool IsSubscribed { get; set; }
    public Guid SubscribedToId { get; set; }
    public Guid SubscribedById { get; set; }
}