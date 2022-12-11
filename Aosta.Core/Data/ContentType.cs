using System.ComponentModel.DataAnnotations;

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