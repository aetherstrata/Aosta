namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaStatisticsQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.STATISTICS
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
