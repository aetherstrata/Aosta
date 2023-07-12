namespace Aosta.Jikan.Query;

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
        return new JikanQuery(QueryEndpoint);
    }
}