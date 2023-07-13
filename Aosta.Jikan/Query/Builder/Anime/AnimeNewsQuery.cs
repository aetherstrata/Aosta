using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeNewsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.News
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<NewsResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<PaginatedJikanResponse<ICollection<NewsResponse>>>(GetEndpoint(id));
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<NewsResponse>>> Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<NewsResponse>>>(GetEndpoint(id))
            .WithParameter(QueryParameter.Page, page);
    }
}