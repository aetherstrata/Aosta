using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomAnimeQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Anime
    };

    internal static IQuery<BaseJikanResponse<AnimeResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<AnimeResponse>>(QueryEndpoint);
    }
}