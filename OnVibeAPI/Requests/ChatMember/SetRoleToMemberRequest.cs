using Domain.Entities;

namespace OnVibeAPI.Requests.ChatMember;

public class SetRoleToMemberRequest
{
    public ChatRoles Role { get; set; }
}