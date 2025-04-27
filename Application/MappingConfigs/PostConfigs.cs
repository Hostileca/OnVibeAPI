using Application.Dtos.Post;
using Application.Helpers;
using Application.UseCases.Post.Commands.Create;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class PostConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePostCommand, Post>()
            .Ignore(dest => dest.Attachments)
            .Map(dest => dest.Date, _ => DateTime.UtcNow)
            .AfterMapping((src, dest) => 
            {
                if (src.Attachments is null)
                {
                    return;
                }
                
                dest.Attachments = src.Attachments
                    .Select(file => new PostAttachment
                    {
                        FileName = file.FileName,
                        Data = Base64Converter.ConvertToBase64(file),
                        ContentType = MimeTypes.GetMimeType(file.FileName)
                    })
                    .ToList();
            });

        config.NewConfig<Post, PostReadDto>()
            .Map(dest => dest.Owner, src => src.User)
            .Map(dest => dest.AttachmentsIds, src => src.Attachments.Select(a => a.Id), 
                src => src.Attachments != null);
    }
}