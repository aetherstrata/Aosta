using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class TopReviewsQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.Reviews
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery Create(TopReviewsType type)
    {
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Type, type);
    }

    internal static IQuery Create(int page, TopReviewsType type)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(type, nameof(type));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Type, type);
    }

    internal static IQuery Create(TopReviewsQueryParameters parameters)
    {
        return new JikanQuery(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}