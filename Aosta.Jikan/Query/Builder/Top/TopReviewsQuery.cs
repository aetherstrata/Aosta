using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Top;

internal static class TopReviewsQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.REVIEWS
    ];

    internal static IQuery Create()
    {
        return JikanQuery.Create(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.PAGE, page);
    }

    internal static IQuery Create(TopReviewsTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.TYPE, type);
    }

    internal static IQuery Create(int page, TopReviewsTypeFilter type)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(type, nameof(type));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.PAGE, page)
            .With(QueryParameter.TYPE, type);
    }

    internal static IQuery Create(TopReviewsQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
