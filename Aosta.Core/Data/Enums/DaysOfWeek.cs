using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

[Flags]
public enum DaysOfWeek
{
    None = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64
}

public static class DaysOfWeekExtensions
{
    public static string ToStringCached(this DaysOfWeek day) => day switch
    {
        DaysOfWeek.Monday => "Mondays",
        DaysOfWeek.Tuesday => "Tuesdays",
        DaysOfWeek.Wednesday => "Wednesdays",
        DaysOfWeek.Thursday => "Thursdays",
        DaysOfWeek.Friday => "Fridays",
        DaysOfWeek.Saturday => "Saturdays",
        DaysOfWeek.Sunday => "Sundays",
        DaysOfWeek.None => "None",
        _ => throw new UnreachableException()
    };
}