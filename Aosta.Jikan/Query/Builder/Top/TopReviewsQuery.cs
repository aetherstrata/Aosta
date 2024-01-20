using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Top;

internal static class TopReviewsQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.REVIEWS
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

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(TopReviewsTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.TYPE, type);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(int page, TopReviewsTypeFilter type)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page)
            .WithParameter(QueryParameter.TYPE, type);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(TopReviewsQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
