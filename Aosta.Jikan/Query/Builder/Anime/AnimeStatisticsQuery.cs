using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeStatisticsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.STATISTICS
    };

    internal static IQuery<BaseJikanResponse<AnimeStatisticsResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<AnimeStatisticsResponse>>(getEndpoint(id));
    }
}
