using Aosta.Common.Extensions;
using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Season;

internal static class SeasonQuery
{
    private static string[] getEndpoint(int year, Jikan.Enums.Season season) => new []
    {
        JikanEndpointConsts.SEASONS,
        year.ToString(),
        season.StringValue()
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int year, Jikan.Enums.Season season)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(getEndpoint(year, season));
    }

    public static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int year, Jikan.Enums.Season season, int page)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(getEndpoint(year, season))
            .WithParameter(QueryParameter.PAGE, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>> Create(int year, Jikan.Enums.Season season, SeasonQueryParameters parameters)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        return new JikanQuery<PaginatedJikanResponse<ICollection<AnimeResponse>>>(getEndpoint(year, season))
            .WithParameterRange(parameters);
    }
}
