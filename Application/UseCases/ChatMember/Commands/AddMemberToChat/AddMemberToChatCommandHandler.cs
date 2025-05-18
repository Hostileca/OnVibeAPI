using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Helpers.PermissionsHelpers;
using Application.Services.Interfaces;
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
    IChatNotificationService chatNotificationService,
    IExtraLoader<ChatReadDto> chatExtraLoader) 
    : IRequestHandler<AddMemberToChatCommand, ChatReadDto>
{
    private static string GetAddMessage(Domain.Entities.User initiator, Domain.Entities.User user) => $"{user.Username} was added by {initiator.Username}";
    
    public async Task<ChatReadDto> Handle(AddMemberToChatCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId, 
            new ChatIncludes
            {
                IncludeChatMembers = true,
            }, 
            cancellationToken,
            true);
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }
        
        var initiatorMember = chat.Members.FirstOrDefault(member => member.UserId == request.InitiatorId);
        if (initiatorMember is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }
        if (!ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to add members to this chat");
        }

        var currentDate = DateTime.UtcNow;
        var chatReadDto = chat.Adapt<ChatReadDto>();
        await chatExtraLoader.LoadExtraInformationAsync(chatReadDto, cancellationToken);
        var existingChatMember = chat.Members.FirstOrDefault(m => m.UserId == request.UserId);
        await chatMembersRepository.LoadUser(initiatorMember, cancellationToken);
        if (existingChatMember is not null)
        {
            if(!existingChatMember.IsRemoved)
            {
                throw new ConflictException("User is already a member of this chat");
            }

            existingChatMember.RemoveDate = null;
            await chatMembersRepository.LoadUser(existingChatMember, cancellationToken);
            var backMessage = new Domain.Entities.Message
            {
                Date = currentDate,
                ChatId = chat.Id,
                Text = GetAddMessage(initiatorMember.User, existingChatMember.User)
            };
        
            await messageRepository.AddAsync(backMessage, cancellationToken);
            await chatRepository.SaveChangesAsync(cancellationToken);
                
            await chatNotificationService.SendMessageToGroupAsync(backMessage.Adapt<MessageReadDto>(), cancellationToken);
            await chatNotificationService.AddMemberToGroupAsync(existingChatMember, chatReadDto, cancellationToken);
        
            return chatReadDto;
        }
        
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken, true);
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }

        var newMember = new Domain.Entities.ChatMember
        {
            Chat = chat,
            ChatId = chat.Id,
            User = user,
            UserId = user.Id,
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
        chat.Members.Add(newMember);
        await chatRepository.SaveChangesAsync(cancellationToken);

        await chatNotificationService.AddMemberToGroupAsync(newMember, chatReadDto, cancellationToken);
        await chatNotificationService.SendMessageToGroupAsync(addMessage.Adapt<MessageReadDto>(), cancellationToken);
        
        return chatReadDto;
    }
}