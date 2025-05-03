namespace Contracts.DataAccess.Models.Include;

public class MessageIncludes
{
    public bool IncludeReactions { get; set; }
    public bool IncludeSender { get; set; }
    public bool ExcludeDelayed { get; set; }
}