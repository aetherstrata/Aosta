using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Producer;

internal static class ProducerExternalLinksQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PRODUCERS,
        id.ToString(),
        JikanEndpointConsts.EXTERNAL
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
