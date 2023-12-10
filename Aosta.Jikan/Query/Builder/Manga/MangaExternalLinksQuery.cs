using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class MangaExternalLinksQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.EXTERNAL
    };

    internal static IQuery<BaseJikanResponse<ICollection<ExternalLinkResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ExternalLinkResponse>>>(getEndpoint(id));
    }
}
