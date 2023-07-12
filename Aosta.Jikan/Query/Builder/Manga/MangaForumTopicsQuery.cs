namespace Aosta.Jikan.Query;

internal static class MangaForumTopicsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString(),
        JikanEndpointConsts.Forum
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}