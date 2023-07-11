using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class RandomMangaQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}