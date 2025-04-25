using Application.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.User.Commands.UpdateUserProfile;

public class UpdateUserProfileCommand : IRequest<UserReadDto>
{
    public Guid Id { get; set; }
    public string? BIO { get; set; }
    public IFormFile? Avatar { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? City { get; set; }
}