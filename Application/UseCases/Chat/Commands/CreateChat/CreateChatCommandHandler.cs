using Application.Dtos.Chat;
using Application.Dtos.Message;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Commands.CreateChat;

public class CreateChatCommandHandler(
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IChatNotificationService chatNotificationService,
    IExtraLoader<ChatReadDto> chatExtraLoader) 
    : IRequestHandler<CreateChatCommand, ChatReadDto>
{
    private static string GetCreateMessage(Domain.Entities.User initiator, Domain.Entities.Chat chat) => $"{initiator.Username} created a chat {chat.Name}";
    
    public async Task<ChatReadDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserIds.Contains(request.InitiatorId))
        {
            request.UserIds.Add(request.InitiatorId);
        }
        
        var users = await userRepository.GetUsersByIdsAsync(request.UserIds, cancellationToken);
        
        if (users.Count != request.UserIds.Count)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserIds.ToString());
        }

        var currentDate = DateTime.UtcNow;
        var chat = request.Adapt<Domain.Entities.Chat>();
        chat.Members = users.Select(u => 
            new Domain.Entities.ChatMember
            {
                ChatId = chat.Id,
                UserId = u.Id,
                JoinDate = currentDate,
                Role = u.Id == request.InitiatorId ? ChatRole.Admin : ChatRole.Member
            }).ToList();
        var createMessage = new Domain.Entities.Message
        {
            Date = currentDate,
            ChatId = chat.Id,
            Text = GetCreateMessage(users.FirstOrDefault(u => u.Id == request.InitiatorId), chat)
        };
        chat.Messages = new List<Domain.Entities.Message> { createMessage };

        await chatRepository.AddChatAsync(chat, cancellationToken);
        await chatRepository.SaveChangesAsync(cancellationToken);

        var chatReadDto = chat.Adapt<ChatReadDto>();
        await chatExtraLoader.LoadExtraInformationAsync(chatReadDto, cancellationToken);
        await chatNotificationService.AddMembersToGroupAsync(chat.Members, chatReadDto, cancellationToken);
        await chatNotificationService.SendMessageToGroupAsync(createMessage.Adapt<MessageReadDto>(), cancellationToken);
        
        return chatReadDto;
    }
}