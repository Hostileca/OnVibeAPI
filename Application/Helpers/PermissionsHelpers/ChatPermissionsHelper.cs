using Domain.Entities;

namespace Application.Helpers.PermissionsHelpers;

public static class ChatPermissionsHelper
{
    public static bool IsUserHasAccessToChat(Chat chat, Guid userId)
    {
        return chat.Members.Any(cm => cm.UserId == userId);
    }
    
    public static bool IsUserHasAccessToManageChat(Chat chat, Guid userId)
    {
        return chat.Members.Any(cm => cm.UserId == userId && cm.Role != ChatRole.Member);
    }
}