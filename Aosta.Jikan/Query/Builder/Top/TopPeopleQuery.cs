namespace Aosta.Jikan.Query.Builder.Top;

internal static class TopPeopleQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.PEOPLE
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

    internal static IQuery Create(int page, int limit)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.PAGE, page)
            .With(QueryParameter.LIMIT, limit);
    }
}
