namespace Aosta.Jikan.Query.Builder.Watch;

internal static class WatchRecentEpisodesQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.WATCH,
        JikanEndpointConsts.EPISODES
    ];

    internal static IQuery Create()
    {
        return JikanQuery.Create(s_QueryEndpoint);
    }
}
