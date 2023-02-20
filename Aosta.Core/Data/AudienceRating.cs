using System.Diagnostics;

namespace Aosta.Core.Data;

public enum AudienceRating
{
    Everyone,
    Children,
    Teens,
    ViolenceProfanity,
    MildNudity,
    Hentai
}

public static class AudienceRatingExtensions
{
    public static string ToStringCached(this AudienceRating ar) => ar switch
    {
        AudienceRating.Everyone => "G - All Ages",
        AudienceRating.Children => "PG - Children",
        AudienceRating.Teens => "PG-13 - Teens 13 or older",
        AudienceRating.ViolenceProfanity => "R - 17+ (violence & profanity)",
        AudienceRating.MildNudity => "R+ - Mild Nudity",
        AudienceRating.Hentai => "Rx - Hentai",
        _ => throw new UnreachableException()
    };
}