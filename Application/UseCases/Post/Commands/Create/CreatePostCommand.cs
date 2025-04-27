using Application.Dtos.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Post.Commands.Create;

public sealed class CreatePostCommand : IRequest<PostReadDto>
{
    public Guid InitiatorId { get; init; }
    public string? Content { get; init; }
    public IList<IFormFile>? Attachments { get; init; }
}