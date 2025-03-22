using Domain.Entities;
using Domain.Entities.Notifications;
using Mapster;

namespace Application.MappingConfigs;

public class NotificationConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Message, MessageNotification>()
            .Map(dest => dest.MessageId, src => src.Id)
            .Map(dest => dest.Message, src => src)
            .Map(dest => dest.Date, _ => DateTime.UtcNow)
            .Map(dest => dest.IsRead, _ => false)
            .Map(dest => dest.Type, _ => NotificationType.Message)
            .Ignore(dest => dest.User)
            .Ignore(dest => dest.UserId);
    }
}