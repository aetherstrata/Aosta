namespace Aosta.Jikan.Query;

internal static class TopPeopleQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.People
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

    internal static IQuery Create(int page, int limit)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, ParameterConsts.MaximumPageSize, nameof(limit));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Limit, limit);
    }
}