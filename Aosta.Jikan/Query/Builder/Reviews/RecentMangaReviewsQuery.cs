using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RecentMangaReviewsQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.REVIEWS,
        JikanEndpointConsts.MANGA
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }
}
