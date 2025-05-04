using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Helpers.PermissionsHelpers;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.AddMemberToChat;

public class AddMemberToChatCommandHandler(
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IMessageRepository messageRepository,
    IChatMembersRepository chatMembersRepository,
    IChatNotificationService chatNotificationService) 
    : IRequestHandler<AddMemberToChatCommand, ChatReadDto>
{
    private static string GetAddMessage(Domain.Entities.User initiator, Domain.Entities.User user) => $"{user.Username} was added by {initiator.Username}";
    
    public async Task<ChatReadDto> Handle(AddMemberToChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId, 
            new ChatIncludes(), 
            cancellationToken);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }
        
        var initiatorMember = await chatMembersRepository.GetChatMemberAsync(
            request.InitiatorId,
            request.ChatId,
            new ChatMemberIncludes { IncludeUser = true },
            cancellationToken,
            true);

        if (initiatorMember is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }
        
        if (ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to add members to this chat");
        }

        var chatMembers = await chatMembersRepository.GetChatMembersAsync(request.ChatId, new ChatMemberIncludes{ IncludeUser = true }, cancellationToken, false);

        var currentDate = DateTime.UtcNow;
        var existingChatMember = chatMembers.FirstOrDefault(m => m.UserId == request.UserId);
        if (existingChatMember is not null)
        {
            if(!existingChatMember.IsRemoved)
            {
                throw new ConflictException("User is already a member of this chat");
            }
            else
            {
                var backMessage = new Domain.Entities.Message
                {
                    Date = currentDate,
                    ChatId = chat.Id,
                    Text = GetAddMessage(initiatorMember.User, existingChatMember.User)
                };
        
                await messageRepository.AddAsync(backMessage, cancellationToken);
                await chatRepository.SaveChangesAsync(cancellationToken);
                
                await chatNotificationService.SendMessageToGroupAsync(backMessage.Adapt<MessageReadDto>(), cancellationToken);
                await chatNotificationService.AddMemberToGroupAsync(existingChatMember, cancellationToken);
        
                return chat.Adapt<ChatReadDto>();
            }
        }
        
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken, true);
        
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }

        var newMember = new Domain.Entities.ChatMember
        {
            Chat = chat,
            User = user,
            JoinDate = currentDate,
            Role = ChatRole.Member
        };
        var addMessage = new Domain.Entities.Message
        {
            Date = currentDate,
            ChatId = chat.Id,
            Text = GetAddMessage(initiatorMember.User, newMember.User)
        };
        
        await messageRepository.AddAsync(addMessage, cancellationToken);
        await chatMembersRepository.AddChatMemberAsync(newMember, cancellationToken);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        await chatNotificationService.AddMemberToGroupAsync(newMember, cancellationToken);
        await chatNotificationService.SendMessageToGroupAsync(addMessage.Adapt<MessageReadDto>(), cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}