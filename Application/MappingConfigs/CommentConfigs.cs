using Application.Dtos.Comment;
using Application.UseCases.Comment.Commands.SendCommentToPost;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigs;

public class CommentConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SendCommentToPostCommand, Comment>()
            .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
            .Map(dest => dest.UserId, src => src.InitiatorId);
        
        config.NewConfig<Comment, CommentReadDto>()
            .Map(dest => dest.UserShortReadDto, src => src.User);
    }
}