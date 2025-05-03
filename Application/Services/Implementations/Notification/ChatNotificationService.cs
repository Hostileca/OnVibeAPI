using Application.Dtos.Message;
using Application.Services.Interfaces;
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
    
    public async Task SendMessageAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken)
    {
        var chatMembers = await chatMembersRepository.GetChatMembersAsync(messageReadDto.ChatId, cancellationToken);
        
        foreach (var member in chatMembers)
        {
            if (member.UserId == messageReadDto.Sender.Id)
            {
                continue;
            }
            
            var notification = member.Adapt<MessageNotification>();
            notification.UserId = member.UserId;
            
            await notificationRepository.AddAsync(notification, cancellationToken);
        }
        
        await chatHub.Clients.Group(GetGroupName(messageReadDto.ChatId)).SendAsync(
            ChatHubEvents.MessageSent, messageReadDto, cancellationToken);
    }

    public async Task RemoveMemberAsync(ChatMember member, CancellationToken cancellationToken)
    {
        var connectionsIds = await connectionRepository.GetConnectionsAsync(member.UserId);
        var removeTasks = connectionsIds.Select(connectionId => chatHub.Groups.RemoveFromGroupAsync(connectionId, GetGroupName(member.ChatId), cancellationToken));
        await Task.WhenAll(removeTasks);
    }
}