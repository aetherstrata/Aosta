namespace Aosta.Jikan.Query.Builder.Random;

internal static class RandomMangaQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.MANGA
    ];

    internal static IQuery Create()
    {
        return JikanQuery.Create(s_QueryEndpoint);
    }
}
