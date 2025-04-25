using Domain.Entities;

namespace Application.Dtos.User;

public class UserReadDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? BIO { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? City { get; set; }
    public Roles Role { get; set; }
}