using System.Diagnostics;
using Aosta.Core.Jikan.Enums;

namespace Aosta.Core.Data.Enums;

[Flags]
public enum GroupSeason
{
    None = 0,
    Winter = 1,
    Spring = 2,
    Summer = 4,
    Fall = 8
}

internal static class SeasonExtensions
{
    internal static string ToStringCached(this GroupSeason groupSeason) => groupSeason switch
    {
        GroupSeason.None => "None",
        GroupSeason.Winter => "Winter",
        GroupSeason.Spring => "Spring",
        GroupSeason.Summer => "Summer",
        GroupSeason.Fall => "Fall",
        _ => throw new UnreachableException()
    };

    internal static GroupSeason ToGroupEnum(this Season jikan) => jikan switch
    {
        Season.Winter => GroupSeason.Winter,
        Season.Fall => GroupSeason.Fall,
        Season.Spring => GroupSeason.Spring,
        Season.Summer => GroupSeason.Summer,
        _ => GroupSeason.None
    };
}