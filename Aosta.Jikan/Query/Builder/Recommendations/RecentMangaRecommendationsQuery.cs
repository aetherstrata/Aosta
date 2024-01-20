using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Recommendations;

internal static class RecentMangaRecommendationsQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.RECOMMENDATIONS,
        JikanEndpointConsts.MANGA
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }
}
