using Application.Helpers;
using Application.UseCases.Chat.Commands.CreateChat;
using Application.UseCases.Chat.Commands.UpdateChat;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class ChatConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateChatCommand, Chat>()
            .Map(dest => dest.Image, src => src.Image != null ? Base64Converter.ConvertToBase64(src.Image) : null);

        config.NewConfig<UpdateChatCommand, Chat>()
            .Map(dest => dest.Image, src => src.Image != null ? Base64Converter.ConvertToBase64(src.Image) : null);
    }
}