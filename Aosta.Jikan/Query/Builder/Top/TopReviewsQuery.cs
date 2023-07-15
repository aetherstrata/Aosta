using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class TopReviewsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.Reviews
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

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(TopReviewsTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Type, type);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(int page, TopReviewsTypeFilter type)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Type, type);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>> Create(TopReviewsQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ReviewResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}