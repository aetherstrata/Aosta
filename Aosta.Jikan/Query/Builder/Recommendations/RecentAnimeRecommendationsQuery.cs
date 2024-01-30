namespace Aosta.Jikan.Query.Builder.Recommendations;

internal static class RecentAnimeRecommendationsQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.RECOMMENDATIONS,
        JikanEndpointConsts.ANIME
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page);
    }
}
