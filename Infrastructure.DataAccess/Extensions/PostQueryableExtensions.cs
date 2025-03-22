using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

internal static class PostQueryableExtensions
{
    public static IQueryable<Post> IncludeUser(
        this IQueryable<Post> posts,
        bool includeUser)
    {
        if (!includeUser)
        {
            return posts;
        }

        return posts.Include(post => post.User);
    }
}