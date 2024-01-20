using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Anime;

internal static class AnimeUserUpdatesQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.USER_UPDATES
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>>(getEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeUserUpdateResponse>>>(getEndpoint(id))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
