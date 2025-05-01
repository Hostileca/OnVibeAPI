using Domain.Entities;

namespace Application.Dtos.User;

public class UserReadDto : UserShortReadDto
{
    public string? BIO { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public Roles Role { get; set; }
    public int PostsCount { get; set; }
    public int SubscriptionsCount { get; set; }
    public int SubscribersCount { get; set; }
    public bool IsSubscribed { get; set; }
}