namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaUserUpdatesQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.USER_UPDATES
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }

    internal static IQuery Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(getEndpoint(id))
            .With(QueryParameter.PAGE, page);
    }
}
