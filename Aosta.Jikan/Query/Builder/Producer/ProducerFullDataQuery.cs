using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ProducerFullDataQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Producers,
        id.ToString(),
        JikanEndpointConsts.Full
    };

    internal static IQuery<BaseJikanResponse<ProducerResponseFull>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ProducerResponseFull>>(GetEndpoint(id));
    }
}