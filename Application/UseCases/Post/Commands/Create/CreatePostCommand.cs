using Application.Dtos.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Post.Commands.Create;

public sealed record CreatePostCommand(Guid UserId, string? Content, IList<IFormFile>? Attachments) : IRequest<PostReadDto>;