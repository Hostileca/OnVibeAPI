namespace OnVibeAPI.Requests.User;

public class UpdateUserProfileRequest
{
    public string? BIO { get; set; }
    public IFormFile? Avatar { get; set; }
}