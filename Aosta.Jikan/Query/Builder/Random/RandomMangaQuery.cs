using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomMangaQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Manga
    };

    internal static IQuery<BaseJikanResponse<MangaResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<MangaResponse>>(QueryEndpoint);
    }
}