namespace Aosta.Jikan.Query;

internal static class WatchRecentPromosQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Promos
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}