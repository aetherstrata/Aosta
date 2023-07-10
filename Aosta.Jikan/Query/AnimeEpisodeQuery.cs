using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query;

internal sealed class AnimeEpisodeQuery : Query
{
    private static string[] QueryEndpoint(long animeId, int episodeId) => new []
    {
        JikanEndpointConsts.Anime,
        animeId.ToString(),
        JikanEndpointConsts.Episodes,
        episodeId.ToString()
    };

    private AnimeEpisodeQuery(long animeId, int episodeId) : base(QueryEndpoint(animeId, episodeId))
    {
    }

    internal static AnimeEpisodeQuery Create(long animeId, int episodeId)
    {
        Guard.IsGreaterThanZero(animeId, nameof(animeId));
        Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
        return new AnimeEpisodeQuery(animeId, episodeId);
    }
}