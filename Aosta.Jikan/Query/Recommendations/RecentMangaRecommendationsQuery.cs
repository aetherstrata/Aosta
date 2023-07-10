using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Recommendations;

internal sealed class RecentMangaRecommendationsQuery : Query
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Recommendations,
        JikanEndpointConsts.Manga
    };

    private RecentMangaRecommendationsQuery() : base(QueryEndpoint)
    {
    }

    private RecentMangaRecommendationsQuery(int page) : this()
    {
        Parameters.Add(new QueryParameter<int>
        {
            Name = QueryParameter.Page,
            Value = page
        });
    }

    internal static RecentMangaRecommendationsQuery Create()
    {
        return new RecentMangaRecommendationsQuery();
    }

    internal static RecentMangaRecommendationsQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new RecentMangaRecommendationsQuery(page);
    }
}