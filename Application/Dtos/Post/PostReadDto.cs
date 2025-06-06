﻿using Application.Dtos.User;

namespace Application.Dtos.Post;

public class PostReadDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public DateTime Date { get; set; }
    public UserShortReadDto Owner { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public bool IsLiked { get; set; }
    public IList<Guid>? AttachmentsIds { get; set; }
}