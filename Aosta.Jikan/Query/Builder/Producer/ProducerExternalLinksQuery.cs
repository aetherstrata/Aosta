using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class ProducerExternalLinksQuery
{
    private static string[] GetEndpoint(long id) => new[]
    {
        JikanEndpointConsts.Producers,
        id.ToString(),
        JikanEndpointConsts.External
    };

    internal static IQuery<BaseJikanResponse<ICollection<ExternalLinkResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(GetEndpoint(id));
    }
}