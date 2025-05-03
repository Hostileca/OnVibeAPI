using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
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
    
    public async Task SendMessageToGroupAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken)
    {
        var chatMembers = await chatMembersRepository.GetChatMembersAsync(messageReadDto.ChatId, new ChatMemberIncludes(), cancellationToken);
        
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

    public async Task RemoveMemberFromGroupAsync(ChatMember member, CancellationToken cancellationToken)
    {
        var connectionsIds = await connectionRepository.GetConnectionsAsync(member.UserId);
        var removeTasks = connectionsIds.Select(connectionId => chatHub.Groups.RemoveFromGroupAsync(connectionId, GetGroupName(member.ChatId), cancellationToken));
        await Task.WhenAll(removeTasks);
    }

    public async Task AddMemberToGroupAsync(ChatMember member, CancellationToken cancellationToken)
    {
        await AddMemberConnectionsToGroupAsync(member, cancellationToken);
        await chatHub.Clients.Group(GetGroupName(member.Chat.Id)).SendAsync(
            ChatHubEvents.ChatAdded, member.Chat.Adapt<ChatReadDto>(), cancellationToken);
    }
    
    public async Task AddMembersToGroupAsync(IEnumerable<ChatMember> members, CancellationToken cancellationToken)
    {
        var addTasks = members.Select(member => AddMemberConnectionsToGroupAsync(member, cancellationToken));
        var chat = members.FirstOrDefault().Chat;
        await Task.WhenAll(addTasks);
        await chatHub.Clients.Group(GetGroupName(chat.Id)).SendAsync(
            ChatHubEvents.ChatAdded, chat.Adapt<ChatReadDto>(), cancellationToken);
    }

    private async Task AddMemberConnectionsToGroupAsync(ChatMember member, CancellationToken cancellationToken)
    {
        var connectionsIds = await connectionRepository.GetConnectionsAsync(member.UserId);
        var addTasks = connectionsIds.Select(connectionId => chatHub.Groups.AddToGroupAsync(connectionId, GetGroupName(member.ChatId), cancellationToken));
        await Task.WhenAll(addTasks);
    }
}