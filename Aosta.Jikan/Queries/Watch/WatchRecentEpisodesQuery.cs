using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class WatchRecentEpisodesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Episodes
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}