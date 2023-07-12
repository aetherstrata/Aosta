namespace Aosta.Jikan.Query;

internal static class WatchRecentEpisodesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Watch,
        JikanEndpointConsts.Episodes
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}