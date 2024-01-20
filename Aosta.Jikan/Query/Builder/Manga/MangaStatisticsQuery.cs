using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaStatisticsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.STATISTICS
    };

    internal static IQuery<BaseJikanResponse<MangaStatisticsResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<MangaStatisticsResponse>>(getEndpoint(id));
    }
}
