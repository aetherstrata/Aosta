namespace Aosta.Jikan.Query.Builder.Watch;

internal static class WatchPopularEpisodesQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.WATCH,
        JikanEndpointConsts.EPISODES,
        JikanEndpointConsts.POPULAR
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }
}
