using System.Security.Claims;
using Contracts.DataAccess.Interfaces;
using Contracts.Redis.Repositories;
using Contracts.SignalR.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.Hubs;

[Authorize]
public class ChatHub(
    IConnectionRepository connectionRepository,
    IChatRepository chatRepository) 
    : Hub
{
    private Guid UserId => new(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    public override async Task OnConnectedAsync()
    {
        await connectionRepository.AddConnectionAsync(UserId, Context.ConnectionId);

        await JoinChatsAsync();
        await base.OnConnectedAsync();
    }

    private async Task JoinChatsAsync()
    {
        var chatsIds = await chatRepository.GetAllUserChatsIds(UserId);
        
        foreach (var chatId in chatsIds)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{Prefixes.Chat}{chatId}");
        }
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await connectionRepository.RemoveConnectionAsync(UserId, Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}