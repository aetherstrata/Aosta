using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeEpisodesQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Episodes
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(GetEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeEpisodeResponse>>>(GetEndpoint(id))
            .WithParameter(QueryParameter.Page, page);
    }
}