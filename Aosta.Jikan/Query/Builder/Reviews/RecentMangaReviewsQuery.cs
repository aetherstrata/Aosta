namespace Aosta.Jikan.Query.Builder.Reviews;

internal static class RecentMangaReviewsQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.REVIEWS,
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
