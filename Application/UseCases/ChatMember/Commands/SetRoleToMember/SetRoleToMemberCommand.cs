using Application.Dtos.ChatMember;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.SetRoleToMember;

public class SetRoleToMemberCommand : IRequest<ChatMemberReadDto>
{
    public Guid ChatId { get; init; }
    public Guid UserId { get; init; }
    public ChatRoles Role { get; init; }
    public Guid InitiatorId { get; init; }
}