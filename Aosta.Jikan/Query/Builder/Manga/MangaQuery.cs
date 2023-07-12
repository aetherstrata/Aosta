namespace Aosta.Jikan.Query;

internal static class MangaQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString()

    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}