using Domain.Entities;

namespace OnVibeAPI.Requests.ChatMember;

public class SetRoleToMemberRequest
{
    public ChatRole Role { get; set; }
}