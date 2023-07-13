using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeEpisodeQuery
{
    private static string[] GetEndpoint(long animeId, int episodeId) => new []
    {
        JikanEndpointConsts.Anime,
        animeId.ToString(),
        JikanEndpointConsts.Episodes,
        episodeId.ToString()
    };

    internal static IQuery<BaseJikanResponse<AnimeEpisodeResponse>> Create(long animeId, int episodeId)
    {
        Guard.IsGreaterThanZero(animeId, nameof(animeId));
        Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
        return new JikanQuery<BaseJikanResponse<AnimeEpisodeResponse>>(GetEndpoint(animeId, episodeId));
    }
}