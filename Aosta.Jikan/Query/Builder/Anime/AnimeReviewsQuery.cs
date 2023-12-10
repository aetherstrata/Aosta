using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeReviewsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.REVIEWS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(getEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(getEndpoint(id))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
