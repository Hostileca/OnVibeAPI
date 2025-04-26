namespace OnVibeAPI.Requests.User;

public class SearchUsersByCriteriaRequest
{
    public string? Username { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}