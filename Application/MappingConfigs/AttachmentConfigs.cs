using Application.Dtos.Attachment;
using Application.Helpers;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class AttachmentConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AttachmentBase, AttachmentReadDto>()
            .Map(dest => dest.Data, src => Base64Converter.ConvertToByteArray(src.Data));
    }
}