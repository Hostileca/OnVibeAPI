using Application.Dtos.Message;
using Application.Dtos.User;
using Application.Helpers.PermissionsHelpers;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Notification;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities.Notifications;
using Domain.Exceptions;
using Hangfire;
using Mapster;
using MediatR;

namespace Application.UseCases.Message.Commands.SendMessage;

public class SendMessageCommandHandler(
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IChatNotificationService chatNotificationService,
    IUserRepository userRepository,
    INotificationRepository notificationRepository,
    IExtraLoader<MessageReadDto> messageExtraLoader)
    : IRequestHandler<SendMessageCommand, MessageReadDtoBase>
{
    public async Task<MessageReadDtoBase> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId,
            new ChatIncludes { IncludeChatMembers = true },
            cancellationToken);

        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }

        var initiator = await userRepository.GetUserByIdAsync(request.InitiatorId, cancellationToken);

        if (initiator is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.InitiatorId.ToString());
        }

        if (!ChatPermissionsHelper.IsUserHasAccessToChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have access to this chat");
        }

        var message = request.Adapt<Domain.Entities.Message>();
        if (request.Date.HasValue)
        {
            message.Date = request.Date.Value;
        }

        await messageRepository.AddAsync(message, cancellationToken);
        await CreateNotifications(message, chat.Members, cancellationToken);
        await messageRepository.SaveChangesAsync(cancellationToken);

        var messageReadDto = message.Adapt<MessageReadDto>();
        messageReadDto.Sender = initiator.Adapt<UserReadDto>();
        await messageExtraLoader.LoadExtraInformationAsync(messageReadDto, cancellationToken);

        if (request.Date.HasValue)
        {
            var jobId = BackgroundJob.Schedule(() => NotifyMessageAsync(messageReadDto, cancellationToken),
                request.Date.Value);
            var scheduledMessageReadDto = message.Adapt<ScheduledMessageReadDto>();
            scheduledMessageReadDto.JobId = jobId;

            return scheduledMessageReadDto;
        }

        await NotifyMessageAsync(messageReadDto, cancellationToken);

        return messageReadDto;
    }

    // It must be public for Hangfire
    public async Task NotifyMessageAsync(MessageReadDto messageReadDto, CancellationToken cancellationToken)
    {
        await chatNotificationService.SendMessageToGroupAsync(messageReadDto, cancellationToken);
    }

    private async Task CreateNotifications(Domain.Entities.Message message, IList<Domain.Entities.ChatMember> chatMembers, CancellationToken cancellationToken)
    {
        foreach (var member in chatMembers)
        {
            if (member.UserId == message.SenderId)
            {
                continue;
            }
            
            var notification = message.Adapt<MessageNotification>();
            notification.UserId = member.UserId;
            
            await notificationRepository.AddAsync(notification, cancellationToken);
        }
    }
}