using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class WatchPopularEpisodesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Episodes,
        JikanEndpointConsts.Popular
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}