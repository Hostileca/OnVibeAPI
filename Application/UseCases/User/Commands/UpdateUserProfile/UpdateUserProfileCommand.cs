using Application.Dtos.User;
using Application.UseCases.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.User.Commands.UpdateUserProfile;

public sealed class UpdateUserProfileCommand : RequestBase<UserReadDto>
{
    public Guid Id { get; init; }
    public string? BIO { get; init; }
    public IFormFile? Avatar { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? City { get; init; }
}