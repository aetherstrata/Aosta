using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Season;

internal static class SeasonQuery
{
    private static string[] getEndpoint(int year, Jikan.Enums.Season season) =>
    [
        JikanEndpointConsts.SEASONS,
        year.ToString(),
        season.StringValue()
    ];

    internal static IQuery Create(int year, Jikan.Enums.Season season)
    {
        Guard.IsValid(x => x is >= 1950 and < 2050, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        return JikanQuery.Create(getEndpoint(year, season));
    }

    public static IQuery Create(int year, Jikan.Enums.Season season, int page)
    {
        Guard.IsValid(x => x is >= 1900 and < 2050, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(getEndpoint(year, season))
            .With(QueryParameter.PAGE, page);
    }

    internal static IQuery Create(int year, Jikan.Enums.Season season, SeasonQueryParameters parameters)
    {
        Guard.IsValid(x => x is >= 1900 and < 2050, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        return JikanQuery.Create(getEndpoint(year, season))
            .AddRange(parameters);
    }
}
