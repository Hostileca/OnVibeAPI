using Mapster;

namespace Infrastructure.Mapster;

public static class CustomAdapter
{
    public static async Task<TDestination> AdaptAsync<TSource, TDestination>(this TSource source)
    {
        return await source.BuildAdapter().AdaptToTypeAsync<TDestination>();
    }
}