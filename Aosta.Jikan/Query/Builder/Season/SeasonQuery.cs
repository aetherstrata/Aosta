using Aosta.Common.Extensions;
using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Query;

internal static class SeasonQuery
{
    private static string[] GetEndpoint(int year, Season season) => new []
    {
        JikanEndpointConsts.Seasons,
        year.ToString(),
        season.StringValue()
    };

    internal static IQuery Create(int year, Season season)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        return new JikanQuery(GetEndpoint(year, season));
    }

    public static IQuery Create(int year, Season season, int page)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(GetEndpoint(year, season))
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery Create(int year, Season season, SeasonQueryParameters parameters)
    {
        Guard.IsValid(x => x is >= 1000 and < 10000, year, nameof(year));
        Guard.IsValidEnum(season, nameof(season));
        return new JikanQuery(GetEndpoint(year, season))
            .WithParameterRange(parameters);
    }
}