namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeStaffQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.STAFF
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
