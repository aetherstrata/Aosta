using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomAnimeQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.ANIME
    };

    internal static IQuery<BaseJikanResponse<AnimeResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<AnimeResponse>>(s_QueryEndpoint);
    }
}
