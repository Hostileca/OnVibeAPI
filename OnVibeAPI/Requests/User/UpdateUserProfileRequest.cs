namespace OnVibeAPI.Requests.User;

public class UpdateUserProfileRequest
{
    public string? BIO { get; set; }
    public IFormFile? Avatar { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}