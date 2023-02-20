using System.Diagnostics;

namespace Aosta.Core.Data;

public enum ContentType
{
    Unknown = -1,
    TV = 0,
    ONA = 1,
    OVA = 2,
    Special = 3,
    Movie = 4,
}

public static class ContentTypeExtensions
{
    public static string ToStringCached(this ContentType contentType) => contentType switch
    {
        ContentType.Unknown => "Unknown",
        ContentType.TV => "TV",
        ContentType.ONA => "ONA",
        ContentType.OVA => "OVA",
        ContentType.Movie => "Movie",
        ContentType.Special => "Special",
        _ => throw new UnreachableException()
    };
}