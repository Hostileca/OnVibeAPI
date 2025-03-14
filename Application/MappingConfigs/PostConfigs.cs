using Application.Dtos.Post;
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
            .Map(dest => dest.Date, _ => DateTime.UtcNow);

        config.NewConfig<Post, PostReadDto>()
            .Map(dest => dest.UserShortReadDto, src => src.User)
            .Map(dest => dest.AttachmentsIds, src => src.Attachments.Select(a => a.Id), 
                src => src.Attachments != null);
    }
}