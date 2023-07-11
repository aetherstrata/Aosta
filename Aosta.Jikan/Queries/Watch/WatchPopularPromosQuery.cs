using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class WatchPopularPromosQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Promos,
        JikanEndpointConsts.Popular
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }
}