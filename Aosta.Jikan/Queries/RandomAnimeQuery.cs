using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class RandomAnimeQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Anime
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}