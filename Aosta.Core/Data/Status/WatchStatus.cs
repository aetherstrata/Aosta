using System.ComponentModel.DataAnnotations;

namespace Aosta.Core.Data;

public enum WatchStatus
{
    [Display(Name = "Completed")]
    Completed,

    [Display(Name = "Dropped")]
    Dropped,

    [Display(Name = "N/A")]
    NotAvailable,

    [Display(Name = "On Hold")]
    OnHold,

    [Display(Name = "Planned")]
    Planned,

    [Display(Name = "Watching")]
    Watching
}