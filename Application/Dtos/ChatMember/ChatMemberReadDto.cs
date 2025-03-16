using Domain.Entities;

namespace Application.Dtos.ChatMember;

public class ChatMemberReadDto
{
    public Guid UserId { get; set; }
    public ChatRoles Role { get; set; }
    public DateTime JoinDate { get; set; }
}