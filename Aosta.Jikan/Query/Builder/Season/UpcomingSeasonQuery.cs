using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Season;

internal static class UpcomingSeasonQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.SEASONS,
        JikanEndpointConsts.UPCOMING
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(SeasonQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
