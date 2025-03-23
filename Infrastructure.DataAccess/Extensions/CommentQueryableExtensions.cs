using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class CommentQueryableExtensions
{
    public static IQueryable<Comment> IncludeUser(
        this IQueryable<Comment> comments,
        bool includeUser)
    {
        if (!includeUser)
        {
            return comments;
        }

        return comments.Include(comment => comment.User);
    }
}