using Application.Dtos.Chat;
using Contracts.DataAccess.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.UseCases.Chat.Commands.CreateChat;

public class CreateChatCommandHandler(
    IChatRepository chatRepository,
    IUserRepository userRepository) 
    : IRequestHandler<CreateChatCommand, ChatReadDto>
{
    public async Task<ChatReadDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserIds.Contains(request.InitiatorId))
        {
            request.UserIds.Add(request.InitiatorId);
        }
        
        var users = await userRepository.GetUsersByIdsAsync(request.UserIds, cancellationToken);
        
        if(users.Count != request.UserIds.Count)
        {
            throw new NotFoundException(typeof(Domain.Entities.User), request.UserIds.ToString());
        }
        
        var chat = request.Adapt<Domain.Entities.Chat>();
        chat.Members = users.Select(u => 
            new Domain.Entities.ChatMember
            {
                UserId = u.Id,
                JoinDate = DateTime.UtcNow,
                Role = u.Id == request.InitiatorId ? ChatRoles.Admin : ChatRoles.Member
            }).ToList();
        
        await chatRepository.AddChatAsync(chat, cancellationToken);
        await chatRepository.SaveChangesAsync(cancellationToken);
        
        return chat.Adapt<ChatReadDto>();
    }
}