using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnVibeAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
    protected Guid UserId => new(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}