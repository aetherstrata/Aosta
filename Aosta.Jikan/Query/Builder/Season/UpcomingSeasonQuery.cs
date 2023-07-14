using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class UpcomingSeasonQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Seasons,
        JikanEndpointConsts.Upcoming
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

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(SeasonQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}