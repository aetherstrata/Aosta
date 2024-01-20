using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.MANGA,
        id.ToString()

    };

    internal static IQuery<BaseJikanResponse<MangaResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<MangaResponse>>(getEndpoint(id));
    }
}
