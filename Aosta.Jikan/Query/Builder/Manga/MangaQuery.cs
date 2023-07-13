using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class MangaQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString()

    };

    internal static IQuery<BaseJikanResponse<MangaResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<MangaResponse>>(GetEndpoint(id));
    }
}