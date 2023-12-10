using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Enums;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class TopAnimeQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.TopList,
        JikanEndpointConsts.Anime
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(TopAnimeFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, filter);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int page, TopAnimeFilter filter)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsValidEnum(filter, nameof(filter));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Filter, filter);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(TopAnimeQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}