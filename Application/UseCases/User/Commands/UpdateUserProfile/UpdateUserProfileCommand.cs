using Application.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.User.Commands.UpdateUserProfile;

public sealed class UpdateUserProfileCommand : IRequest<UserReadDto>
{
    public Guid UserId { get; init; }
    public Guid InitiatorId { get; init; }
    public string? BIO { get; init; }
    public IFormFile? Avatar { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? Country { get; init; }
    public string? City { get; init; }
}