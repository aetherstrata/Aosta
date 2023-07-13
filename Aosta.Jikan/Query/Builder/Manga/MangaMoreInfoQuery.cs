using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class MangaMoreInfoQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString(),
        JikanEndpointConsts.MoreInfo
    };

    internal static IQuery<BaseJikanResponse<MoreInfoResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<MoreInfoResponse>>(GetEndpoint(id));
    }
}