using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnVibeAPI.Controllers;


[ApiController]
[Route(RoutePrefix + "[controller]")]
[Authorize]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected const string RoutePrefix = "api/";
    protected Guid InitiatorId => new(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}