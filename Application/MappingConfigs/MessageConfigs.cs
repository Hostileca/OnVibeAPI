using Application.Dtos.Message;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class MessageConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Message, MessageReadDto>()
            .Map(dest => dest.AttachmentsIds, src => src.Attachments.Select(a => a.Id), 
                src => src.Attachments != null); 
    }
}