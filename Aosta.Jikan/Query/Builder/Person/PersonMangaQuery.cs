namespace Aosta.Jikan.Query;

internal static class PersonMangaQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}