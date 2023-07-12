namespace Aosta.Jikan.Query;

internal static class RecentMangaReviewsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Reviews,
        JikanEndpointConsts.Manga
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