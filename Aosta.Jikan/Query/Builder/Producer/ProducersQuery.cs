using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Producer;

internal static class ProducersQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.PRODUCERS
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

    internal static IQuery Create(ProducersQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
