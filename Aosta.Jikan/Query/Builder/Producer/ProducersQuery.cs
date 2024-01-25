using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
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
        return new JikanQuery(s_QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.PAGE, page);
    }

    internal static IQuery Create(ProducersQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
