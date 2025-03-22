using Application.Dtos.Message;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Domain.Entities;
using Domain.Entities.Notifications;
using Infrastructure.SignalR.Hubs;
using Mapster;
using Microsoft.AspNetCore.SignalR;

namespace Application.Services.Implementations.Notification;

public class MessageNotificationService(
    IHubContext<ChatHub> chatHub,
    INotificationRepository notificationRepository,
    IChatMembersRepository chatMembersRepository) 
    : IMessageNotificationService
{
    private const string ChatPrefix = "chat_";
    
    public async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var chatMembers = await chatMembersRepository.GetChatMembersAsync(message.ChatId, cancellationToken);
        
        foreach (var member in chatMembers)
        {
            if (member.UserId == message.SenderId)
            {
                continue;
            }
            
            var notification = member.Adapt<MessageNotification>();
            notification.UserId = member.UserId;
            
            await notificationRepository.AddAsync(notification, cancellationToken);
        }
        
        await chatHub.Clients.Group($"{ChatPrefix}{message.ChatId}").SendAsync(
            ChatHubEvents.MessageSent, message.Adapt<MessageReadDto>(), cancellationToken);
    }
}