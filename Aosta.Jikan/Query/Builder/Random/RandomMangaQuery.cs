namespace Aosta.Jikan.Query;

internal static class RandomMangaQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}