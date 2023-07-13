using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ProducerQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Producers,
        id.ToString()
    };

    internal static IQuery<BaseJikanResponse<ProducerResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ProducerResponse>>(GetEndpoint(id));
    }
}