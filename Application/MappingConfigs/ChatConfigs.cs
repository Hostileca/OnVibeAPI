using Application.Dtos.Chat;
using Mapster;

namespace Application.MappingConfigs;

public class ChatConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Entities.Chat, ChatReadDto>()
            .Map(dest => dest.MembersIds, src => src.ChatsMembers.Select(cm => cm.UserId));
    }
}