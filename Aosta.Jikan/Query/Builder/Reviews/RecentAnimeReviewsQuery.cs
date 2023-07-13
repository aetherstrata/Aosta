using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RecentAnimeReviewsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Reviews,
        JikanEndpointConsts.Anime
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }
}