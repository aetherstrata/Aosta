using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class RecentMangaReviewsQuery : Query
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Reviews,
        JikanEndpointConsts.Manga
    };

    private RecentMangaReviewsQuery() : base(QueryEndpoint)
    {
    }

    private RecentMangaReviewsQuery(int page) : this()
    {
        Parameters.Add(new QueryParameter<int>
        {
            Name = QueryParameter.Page,
            Value = page
        });
    }

    internal static RecentMangaReviewsQuery Create()
    {
        return new RecentMangaReviewsQuery();
    }

    internal static RecentMangaReviewsQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new RecentMangaReviewsQuery(page);
    }
}