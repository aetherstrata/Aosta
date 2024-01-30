namespace Aosta.Jikan.Query.Builder.Reviews;

internal static class RecentAnimeReviewsQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.REVIEWS,
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
