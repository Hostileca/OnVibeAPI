using Application.Dtos.ChatMember;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.SetRoleToMember;

public record SetRoleToMemberCommand(Guid ChatId, Guid UserId, ChatRoles Role, Guid InitiatorId) : IRequest<ChatMemberReadDto>;