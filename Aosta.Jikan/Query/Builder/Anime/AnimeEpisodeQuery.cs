namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeEpisodeQuery
{
    private static string[] getEndpoint(long animeId, int episodeId) =>
    [
        JikanEndpointConsts.ANIME,
        animeId.ToString(),
        JikanEndpointConsts.EPISODES,
        episodeId.ToString()
    ];

    internal static IQuery Create(long animeId, int episodeId)
    {
        Guard.IsGreaterThanZero(animeId, nameof(animeId));
        Guard.IsGreaterThanZero(episodeId, nameof(episodeId));
        return new JikanQuery(getEndpoint(animeId, episodeId));
    }
}
