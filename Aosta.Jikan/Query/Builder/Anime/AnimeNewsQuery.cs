using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeNewsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.ANIME,
        id.ToString(),
        JikanEndpointConsts.NEWS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<NewsResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<NewsResponse>>>(getEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<NewsResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<NewsResponse>>>(getEndpoint(id))
            .WithParameter(QueryParameter.PAGE, page);
    }
}
