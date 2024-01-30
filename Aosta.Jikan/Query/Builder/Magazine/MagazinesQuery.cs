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
        return new JikanQuery(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page);
    }

    public static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.QUERY, query);
    }

    public static IQuery Create(string query, int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page)
            .Add(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(MagazinesQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
