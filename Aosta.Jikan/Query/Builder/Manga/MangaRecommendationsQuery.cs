using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaRecommendationsQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.RECOMMENDATIONS
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
