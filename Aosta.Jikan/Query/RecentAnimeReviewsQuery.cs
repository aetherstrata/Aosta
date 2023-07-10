using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class RecentAnimeReviewsQuery : Query
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Reviews,
        JikanEndpointConsts.Anime
    };

    private RecentAnimeReviewsQuery() : base(QueryEndpoint)
    {
    }

    private RecentAnimeReviewsQuery(int page) : this()
    {
        Parameters.Add(new QueryParameter<int>
        {
            Name = QueryParameter.Page,
            Value = page
        });
    }

    internal static RecentAnimeReviewsQuery Create()
    {
        return new RecentAnimeReviewsQuery();
    }

    internal static RecentAnimeReviewsQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new RecentAnimeReviewsQuery(page);
    }
}