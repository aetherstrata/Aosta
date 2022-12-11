using System.Diagnostics;
using Aosta.Core.Data;

namespace Aosta.Core.Extensions;

public static class ContentTypeExtensions
{
    public static string ToStringCached(this ContentType contentType) => contentType switch
    {
        ContentType.TV => "TV",
        ContentType.ONA => "ONA",
        ContentType.OVA => "OVA",
        ContentType.Movie => "Movie",
        ContentType.Special => "Special",
        _ => throw new UnreachableException()
    };
}