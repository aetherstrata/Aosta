using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ProducerFullDataQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PRODUCERS,
        id.ToString(),
        JikanEndpointConsts.FULL
    };

    internal static IQuery<BaseJikanResponse<ProducerResponseFull>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ProducerResponseFull>>(getEndpoint(id));
    }
}
