using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Recommendations;

internal sealed class RecentAnimeRecommendationsQuery : Query
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Recommendations,
        JikanEndpointConsts.Anime
    };

    private RecentAnimeRecommendationsQuery() : base(QueryEndpoint)
    {
    }

    private RecentAnimeRecommendationsQuery(int page) : this()
    {
        Parameters.Add(new QueryParameter<int>
        {
            Name = QueryParameter.Page,
            Value = page
        });
    }

    internal static RecentAnimeRecommendationsQuery Create()
    {
        return new RecentAnimeRecommendationsQuery();
    }

    internal static RecentAnimeRecommendationsQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new RecentAnimeRecommendationsQuery(page);
    }
}