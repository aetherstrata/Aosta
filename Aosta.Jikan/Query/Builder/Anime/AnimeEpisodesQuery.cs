using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeEpisodesQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.EPISODES
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(getEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(getEndpoint(id))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
