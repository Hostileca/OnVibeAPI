using Application.Dtos.Post;
using Application.Helpers;
using Contracts.DataAccess.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Post.Commands.Create;

public class CreatePostCommandHandler(
    IUserRepository userRepository,
    IPostRepository postRepository) 
    : IRequestHandler<CreatePostCommand, PostReadDto>
{
    public async Task<PostReadDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }

        var post = request.Adapt<Domain.Entities.Post>();

        await postRepository.AddPostAsync(post, cancellationToken);
        await postRepository.SaveChangesAsync(cancellationToken);
        
        post.User = user;
        
        return post.Adapt<PostReadDto>();
    }
}