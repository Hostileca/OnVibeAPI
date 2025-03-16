using Application.Dtos.ChatMember;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.SetRoleToMember;

public class SetRoleToMemberCommandHandler(
    IChatRepository chatRepository)
    : IRequestHandler<SetRoleToMemberCommand, ChatMemberReadDto>
{
    public async Task<ChatMemberReadDto> Handle(SetRoleToMemberCommand request, CancellationToken cancellationToken)
    {
        var chat = await chatRepository.GetChatByIdAsync(
            request.ChatId, 
            new ChatIncludes{ IncludeChatMembers = true }, 
            cancellationToken, 
            true);
        
        if (chat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.Chat), request.ChatId.ToString());
        }
        
        var initiatorToChat = chat.Members.FirstOrDefault(m => m.UserId == request.InitiatorId);

        if (initiatorToChat is null)
        {
            throw new ForbiddenException("You are not a member of this chat");
        }

        if (initiatorToChat.Role != ChatRoles.Admin)
        {
            throw new ForbiddenException("You don't have permissions to set roles to members of this chat");
        }
        
        var memberToChat = chat.Members.FirstOrDefault(m => m.UserId == request.UserId);

        if (memberToChat is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.ChatMember), request.UserId.ToString());
        }
        
        memberToChat.Role = request.Role;
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return memberToChat.Adapt<ChatMemberReadDto>();
    }
}