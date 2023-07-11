using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class RecentMangaRecommendationsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Recommendations,
        JikanEndpointConsts.Manga
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new Query(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }
}