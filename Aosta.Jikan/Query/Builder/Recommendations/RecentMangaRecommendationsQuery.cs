using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RecentMangaRecommendationsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Recommendations,
        JikanEndpointConsts.Manga
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<UserRecommendationResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }
}