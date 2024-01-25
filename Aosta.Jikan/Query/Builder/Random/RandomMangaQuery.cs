using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Random;

internal static class RandomMangaQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.MANGA
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }
}
