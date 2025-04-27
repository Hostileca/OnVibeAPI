namespace Application.Dtos.Like;

public class LikeReadDto
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public int LikesCount { get; set; }
}