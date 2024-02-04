namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeMoreInfoQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.MORE_INFO
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
