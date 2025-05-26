using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Dtos.Reaction;
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
    IConnectionRepository connectionRepository) 
    : IChatNotificationService
{
    private string GetGroupName(Guid chatId) => $"{Prefixes.Chat}{chatId}";
    
    public async Task SendMessageToGroupAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group(GetGroupName(messageReadDto.ChatId)).SendAsync(
            ChatHubEvents.MessageSent, messageReadDto, cancellationToken);
    }

    public async Task SendMessageReadToGroupAsync(Guid messageId, Guid userId, Guid chatId, CancellationToken cancellationToken)
    {
        await chatHub.Clients.Group(GetGroupName(chatId)).SendAsync(
            ChatHubEvents.MessageRead, 
            new
            {
                ChatId = chatId, 
                MessageId = messageId, 
                UserId = userId
            }, 
            cancellationToken);
    }

    public async Task RemoveMemberFromGroupAsync(ChatMember member, CancellationToken cancellationToken)
    {
        var connectionsIds = await connectionRepository.GetConnectionsAsync(member.UserId);
        var removeTasks = connectionsIds.Select(connectionId => chatHub.Groups.RemoveFromGroupAsync(connectionId, GetGroupName(member.ChatId), cancellationToken));
        await Task.WhenAll(removeTasks);
    }

    public async Task AddMemberToGroupAsync(ChatMember member, ChatReadDto chatReadDto, CancellationToken cancellationToken)
    {
        await AddMemberConnectionsToGroupAsync(member, cancellationToken);
        await chatHub.Clients.Group(GetGroupName(chatReadDto.Id)).SendAsync(
            ChatHubEvents.ChatAdded, chatReadDto, cancellationToken);
    }
    
    public async Task AddMembersToGroupAsync(IEnumerable<ChatMember> members, ChatReadDto chatReadDto, CancellationToken cancellationToken)
    {
        var addTasks = members.Select(member => AddMemberConnectionsToGroupAsync(member, cancellationToken));
        await Task.WhenAll(addTasks);
        await chatHub.Clients.Group(GetGroupName(chatReadDto.Id)).SendAsync(
            ChatHubEvents.ChatAdded, chatReadDto, cancellationToken);
    }

    public async Task SendReactionToGroupAsync(
        ReactionReadDto reactionReadDto, 
        Guid chatId, 
        CancellationToken cancellationToken,
        bool isRemoved = false)
    {
        var eventName = isRemoved ? ChatHubEvents.ReactionRemoved : ChatHubEvents.ReactionSent;

        await chatHub.Clients
            .Group(GetGroupName(chatId))
            .SendAsync(eventName, reactionReadDto, cancellationToken);
    }

    private async Task AddMemberConnectionsToGroupAsync(ChatMember member, CancellationToken cancellationToken)
    {
        var connectionsIds = await connectionRepository.GetConnectionsAsync(member.UserId);
        var addTasks = connectionsIds.Select(connectionId => chatHub.Groups.AddToGroupAsync(connectionId, GetGroupName(member.ChatId), cancellationToken));
        await Task.WhenAll(addTasks);
    }
}