using System.Security.Claims;
using Contracts.Redis.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.Hubs;

[Authorize]
public class ChatHub(IConnectionRepository connectionRepository) : Hub
{
    private Guid UserId => new(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    public override async Task OnConnectedAsync()
    {
        await connectionRepository.AddConnectionAsync(UserId, Context.ConnectionId);
        
        await base.OnConnectedAsync();
    }
}