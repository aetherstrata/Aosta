using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Animeikan.GUI.Enums
{
    internal enum WatchStatus

    {
        [Display(Name = "Completed")]
        [Description("The anime has been completed.")]
        Completed,

        [Display(Name = "Dropped")]
        [Description("The anime has been dropped.")]
        Dropped,

        [Display(Name = "N/A")]
        [Description("The watch status for this content is currently not available.")]
        NotAvailable,

        [Display(Name = "On Hold")]
        [Description("The anime has been put on hold.")]
        OnHold,

        [Display(Name = "Planned")]
        [Description("The anime has been planned for watching.")]
        Planned,

        [Display(Name = "Watching")]
        [Description("The anime is currently being watched.")]
        Watching
    }
}
