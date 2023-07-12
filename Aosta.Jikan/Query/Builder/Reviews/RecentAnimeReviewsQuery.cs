namespace Aosta.Jikan.Query;

internal static class RecentAnimeReviewsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Reviews,
        JikanEndpointConsts.Anime
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }
}