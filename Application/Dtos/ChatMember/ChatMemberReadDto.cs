using Domain.Entities;

namespace Application.Dtos.ChatMember;

public class ChatMemberReadDto
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public ChatRole Role { get; set; }
    public DateTime JoinDate { get; set; }
}