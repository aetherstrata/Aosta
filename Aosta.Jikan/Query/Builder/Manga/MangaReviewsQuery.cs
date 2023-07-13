using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class MangaReviewsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString(),
        JikanEndpointConsts.Reviews
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(GetEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(GetEndpoint(id))
            .WithParameter(QueryParameter.Page, page);
    }
}