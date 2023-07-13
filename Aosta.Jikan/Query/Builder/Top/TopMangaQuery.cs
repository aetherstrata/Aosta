using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query;

internal static class TopMangaQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.Manga
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(TopMangaFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, filter.StringValue());
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(int page, TopMangaFilter filter)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Filter, filter.StringValue());
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MangaResponse>>> Create(TopMangaQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MangaResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}