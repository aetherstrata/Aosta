using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class TopMangaQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.TOP_LIST,
        JikanEndpointConsts.MANGA
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(TopMangaFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.FILTER, filter);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(int page, TopMangaFilter filter)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page)
            .WithParameter(QueryParameter.FILTER, filter);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(TopMangaQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
