using Contracts.SignalR.Dtos;

namespace Contracts.SignalR.NotificationServices;

public interface IMessageNotificationService
{
    Task SendMessageAsync(MessageSendDto messageSendDto, CancellationToken cancellationToken);
}