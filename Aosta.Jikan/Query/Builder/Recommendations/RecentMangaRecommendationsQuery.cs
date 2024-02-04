namespace Aosta.Jikan.Query.Builder.Recommendations;

internal static class RecentMangaRecommendationsQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.RECOMMENDATIONS,
        JikanEndpointConsts.MANGA
    ];

    internal static IQuery Create()
    {
        return JikanQuery.Create(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.PAGE, page);
    }
}
