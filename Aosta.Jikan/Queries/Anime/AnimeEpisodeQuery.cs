using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Queries;

internal static class AnimeEpisodeQuery
{
    private static string[] GetEndpoint(long animeId, int episodeId) => new []
    {
        JikanEndpointConsts.Anime,
        animeId.ToString(),
        JikanEndpointConsts.Episodes,
        episodeId.ToString()
    };

    internal static IQuery Create(long animeId, int episodeId)
    {
        Guard.IsGreaterThanZero(animeId, nameof(animeId));
        Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
        return new Query(GetEndpoint(animeId, episodeId));
    }
}