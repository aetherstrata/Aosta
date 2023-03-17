using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

public enum ContentType
{
    Unknown = -1,
    Movie,
    Music,
    ONA,
    OVA,
    Special,
    TV
}

public static class ContentTypeExtensions
{
    public static string ToStringCached(this ContentType contentType) => contentType switch
    {
        ContentType.Unknown => "Unknown",
        ContentType.Movie => "Movie",
        ContentType.Music => "Music",
        ContentType.ONA => "ONA",
        ContentType.OVA => "OVA",
        ContentType.Special => "Special",
        ContentType.TV => "TV",
        _ => throw new UnreachableException()
    };
}