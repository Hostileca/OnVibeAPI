using Application.Dtos.Message;
using Application.Helpers.PermissionsHelpers;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Contracts.SignalR.Dtos;
using Contracts.SignalR.NotificationServices;
using Domain.Exceptions;
using Hangfire;
using Mapster;
using MediatR;

namespace Application.UseCases.Message.Commands.SendMessage;

public class SendMessageCommandHandler(
    IChatRepository chatRepository,
    IMessageRepository messageRepository,
    IMessageNotificationService messageNotificationService)
    : IRequestHandler<SendMessageCommand, MessageReadDtoBase>
{
    public async Task<MessageReadDtoBase> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.InitiatorId, 
            new ChatIncludes{ IncludeChatMembers = true },
            cancellationToken);

        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }

        if (!ChatPermissionsHelper.IsUserHasAccessToChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have access to this chat");
        }
        
        var message = request.Adapt<Domain.Entities.Message>();
        if (request.Delay.HasValue)
        {
            var jobId = BackgroundJob.Schedule(() => SendMessageAsync(message, cancellationToken), request.Delay.Value);
            var scheduledMessageReadDto = message.Adapt<ScheduledMessageReadDto>();
            scheduledMessageReadDto.JobId = jobId;
            
            return scheduledMessageReadDto;
        }
        
        await SendMessageAsync(message, cancellationToken);
        
        return message.Adapt<MessageReadDto>();
    }
    
    // It must be public for Hangfire
    public async Task SendMessageAsync(Domain.Entities.Message message, CancellationToken cancellationToken)
    {
        message.Date = DateTime.UtcNow;
        
        await messageRepository.AddAsync(message, cancellationToken);
        await messageRepository.SaveChangesAsync(cancellationToken);

        await messageNotificationService.SendMessageAsync(message.Adapt<MessageSendDto>(), cancellationToken);
    }
}