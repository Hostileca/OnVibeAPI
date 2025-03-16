using Application.Dtos.Chat;
using Application.Helpers.PermissionsHelpers;
using Contracts.DataAccess.Interfaces;
using Contracts.DataAccess.Models.Include;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.ChatMember.Commands.AddMemberToChat;

public class AddMemberToChatCommandHandler(
    IChatRepository chatRepository,
    IUserRepository userRepository) 
    : IRequestHandler<AddMemberToChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(AddMemberToChatCommand request, CancellationToken cancellationToken)
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
        
        if (ChatPermissionsHelper.IsUserHasAccessToManageChat(chat, request.InitiatorId))
        {
            throw new ForbiddenException("You don't have permissions to add members to this chat");
        }
        
        if(chat.Members.Any(m => m.UserId == request.UserId))
        {
            throw new ConflictException("User is already a member of this chat");
        }
        
        var user = await userRepository.GetUserByIdAsync(request.UserId, cancellationToken, true);
        
        if (user is null)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserId.ToString());
        }
        
        chat.Members.Add(
            new Domain.Entities.ChatMember
            {
                Chat = chat, 
                User = user,
                JoinDate = DateTime.UtcNow,
                Role = ChatRoles.Member
            });
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}