using Contracts.SignalR.Dtos;
using Contracts.SignalR.NotificationServices;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR.NotificationServices;

internal class MessageNotificationService(IHubContext<ChatHub> chatHub) : IMessageNotificationService
{
    private readonly string _chatPrefix = "chat_";
    
    public async Task SendMessageAsync(MessageSendDto messageSendDto, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group($"{_chatPrefix}{messageSendDto.ChatId}").SendAsync(
            ChatHubEvents.MessageSent, messageSendDto, cancellationToken);
    }
}