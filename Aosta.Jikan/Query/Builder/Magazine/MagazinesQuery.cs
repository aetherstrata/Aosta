using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Magazine;

internal static class MagazinesQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.MAGAZINES
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

    public static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.QUERY, query);
    }

    public static IQuery Create(string query, int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.PAGE, page)
            .With(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(MagazinesQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
