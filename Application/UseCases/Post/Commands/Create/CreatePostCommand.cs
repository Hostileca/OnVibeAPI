using Application.Dtos.Post;
using Application.UseCases.Base;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Post.Commands.Create;

public sealed class CreatePostCommand : RequestBase<PostReadDto>
{
    public string? Content { get; init; }
    public IList<IFormFile>? Attachments { get; init; }
}