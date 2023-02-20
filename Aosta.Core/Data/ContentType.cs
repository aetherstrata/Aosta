using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Aosta.Core.Data;

public enum ContentType
{
    [Display(Name = "TV")]
    TV = 0,

    [Display(Name = "Original Net Animation")]
    ONA = 1,

    [Display(Name = "Original Video Animation")]
    OVA = 2,

    [Display(Name = "Special")]
    Special = 3,

    [Display(Name = "Movie")]
    Movie = 4
}

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