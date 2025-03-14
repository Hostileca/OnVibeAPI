using Contracts.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class BaseQueryableExtensions
{
    public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> query, PageInfo pageInfo)
    {
        return query
            .Skip((pageInfo.PageNumber - 1) * pageInfo.PageSize)
            .Take(pageInfo.PageSize);
    }
    
    public static IQueryable<TEntity> TrackChanges<TEntity>(
        this IQueryable<TEntity> query,
        bool trackChanges)
        where TEntity : class
    {
        if (trackChanges)
        {
            return query;
        }

        return query.AsNoTracking();
    }

}