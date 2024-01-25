using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeVideosQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.VIDEOS
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
