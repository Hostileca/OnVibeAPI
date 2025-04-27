using Application.Dtos.Chat;
using Application.UseCases.Base;
using MediatR;

namespace Application.UseCases.Chat.Commands.DeleteChat;

public sealed class DeleteChatCommand : RequestBase<ChatReadDto>
{
    public Guid ChatId { get; init; }
}