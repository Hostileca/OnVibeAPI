using Application.Dtos.ChatMember;
using Application.UseCases.Base;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.SetRoleToMember;

public class SetRoleToMemberCommand : RequestBase<ChatMemberReadDto>
{
    public Guid ChatId { get; init; }
    public Guid UserId { get; init; }
    public ChatRoles Role { get; init; }
}