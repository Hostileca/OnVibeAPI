using Application.Dtos.Message;
using Application.Helpers;
using Application.UseCases.Message.Commands.SendMessage;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class MessageConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Message, MessageReadDto>()
            .Map(dest => dest.Sender, src => src.Sender, src => src.Sender != null)
            .Map(dest => dest.AttachmentsIds, src => src.Attachments.Select(a => a.Id),
                src => src.Attachments != null);

        config.NewConfig<SendMessageCommand, Message>()
            .Map(dest => dest.SenderId, src => src.InitiatorId)
            .AfterMapping((src, dest) => 
            {
                if (src.Attachments is null)
                {
                    return;
                }
                
                dest.Attachments = src.Attachments
                    .Select(file => new MessageAttachment()
                    {
                        FileName = file.FileName,
                        Data = Base64Converter.ConvertToBase64(file),
                        ContentType = MimeTypes.GetMimeType(file.FileName)
                    })
                    .ToList();
            });
    }
}