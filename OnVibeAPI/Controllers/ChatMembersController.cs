using Application.UseCases.ChatMember.Commands.AddMemberToChat;
using Application.UseCases.ChatMember.Commands.RemoveMemberFromChat;
using Application.UseCases.ChatMember.Commands.SetRoleToMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnVibeAPI.Requests.Chat;
using OnVibeAPI.Requests.ChatMember;

namespace OnVibeAPI.Controllers;

[Route(RoutePrefix + "chats/{chatId:guid}/members")]
public class ChatMembersController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddMemberToChat(Guid chatId, [FromBody] AddMemberToChatRequest addMemberToChatRequest,
        CancellationToken cancellationToken)
    {
        var command = new AddMemberToChatCommand{ ChatId = chatId, UserId = addMemberToChatRequest.UserId, InitiatorId = InitiatorId };
        
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> RemoveMemberFromChat(Guid chatId, Guid userId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveMemberFromChatCommand{ ChatId = chatId, InitiatorId = InitiatorId, UserId = userId };
        
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }
    
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateChatMembers(Guid chatId, Guid userId, [FromBody] SetRoleToMemberRequest setRoleToMemberRequest,
        CancellationToken cancellationToken)
    {
        var command = new SetRoleToMemberCommand{ ChatId = chatId, InitiatorId = InitiatorId, Role = setRoleToMemberRequest.Role, UserId = userId };
        
        var result = await mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}