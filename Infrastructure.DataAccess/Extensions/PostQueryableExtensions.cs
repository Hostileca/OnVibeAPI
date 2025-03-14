using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class PostQueryableExtensions
{
    public static IQueryable<Post> IncludeUser(
        this IQueryable<Post> posts,
        bool includeUsers)
    {
        if (!includeUsers)
        {
            return posts;
        }

        return posts.Include(post => post.User);
    }
}