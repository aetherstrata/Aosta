using System.Diagnostics;
using JikanDotNet;

namespace Aosta.Core.Data.Enums;

[Flags]
public enum Seasons
{
    None = 0,
    Winter = 1,
    Spring = 2,
    Summer = 4,
    Fall = 8
}

internal static class SeasonExtensions
{
    internal static string ToStringCached(this Seasons season)
    {
        return season switch
        {
            Seasons.None => "None",
            Seasons.Winter => "Winter",
            Seasons.Spring => "Spring",
            Seasons.Summer => "Summer",
            Seasons.Fall => "Fall",
            _ => throw new UnreachableException()
        };
    }

    internal static Seasons ToLocalEnum(this Season? jikan)
    {
        return jikan switch
        {
            Season.Fall => Seasons.Fall,
            Season.Spring => Seasons.Spring,
            Season.Summer => Seasons.Summer,
            Season.Winter => Seasons.Winter,
            _ => Seasons.None
        };
    }
}