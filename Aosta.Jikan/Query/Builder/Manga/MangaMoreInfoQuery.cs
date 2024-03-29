namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaMoreInfoQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.MORE_INFO
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
