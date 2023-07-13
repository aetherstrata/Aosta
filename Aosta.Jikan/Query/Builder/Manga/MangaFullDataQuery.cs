using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class MangaFullDataQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString(),
        JikanEndpointConsts.Full
    };

    internal static IQuery<BaseJikanResponse<MangaResponseFull>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<MangaResponseFull>>(GetEndpoint(id));
    }
}