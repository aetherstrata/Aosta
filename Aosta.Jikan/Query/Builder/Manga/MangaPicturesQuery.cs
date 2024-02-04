namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaPicturesQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.PICTURES
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
