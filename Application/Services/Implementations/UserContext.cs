using System.Security.Claims;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Implementations;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid InitiatorId => new(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}