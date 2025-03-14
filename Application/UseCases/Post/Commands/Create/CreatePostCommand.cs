using Application.Dtos.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Post.Commands.Create;

public sealed record CreatePostCommand : IRequest<PostReadDto>
{
    public string? Content { get; set; }
    public Guid UserId { get; set; }
    public IList<IFormFile>? Attachments { get; set; }
}