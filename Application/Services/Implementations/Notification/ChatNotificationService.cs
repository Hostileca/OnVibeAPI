using Application.Dtos.Message;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.Redis.Repositories;
using Contracts.SignalR.Constants;
using Domain.Entities;
using Domain.Entities.Notifications;
using Infrastructure.SignalR.Hubs;
using Mapster;
using Microsoft.AspNetCore.SignalR;

namespace Application.Services.Implementations.Notification;

public class ChatNotificationService(
    IHubContext<ChatHub> chatHub,
    INotificationRepository notificationRepository,
    IChatMembersRepository chatMembersRepository,
    IConnectionRepository connectionRepository) 
    : IChatNotificationService
{
    private string GetGroupName(Guid chatId) => $"{Prefixes.Chat}{chatId}";
    
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
        
        await chatHub.Clients.Group(GetGroupName(message.ChatId)).SendAsync(
            ChatHubEvents.MessageSent, message.Adapt<MessageReadDto>(), cancellationToken);
    }

    public async Task RemoveMemberAsync(ChatMember member, CancellationToken cancellationToken)
    {
        var connectionsIds = await connectionRepository.GetConnectionsAsync(member.UserId);
        var removeTasks = connectionsIds.Select(connectionId => chatHub.Groups.RemoveFromGroupAsync(connectionId, GetGroupName(member.ChatId), cancellationToken));
        await Task.WhenAll(removeTasks);
    }
}