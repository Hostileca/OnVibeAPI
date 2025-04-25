using Application.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.User.Commands.UpdateUserProfile;

public sealed record UpdateUserProfileCommand(Guid Id, string? BIO, IFormFile Avatar, DateTime? DateOfBirth, string? City) : IRequest<UserReadDto>;