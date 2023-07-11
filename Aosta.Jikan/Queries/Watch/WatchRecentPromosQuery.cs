using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class WatchRecentPromosQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Promos
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}