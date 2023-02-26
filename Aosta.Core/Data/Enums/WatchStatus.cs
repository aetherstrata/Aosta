using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

public enum WatchStatus
{
    PlanToWatch = -1,
    Completed,
    Dropped,
    OnHold,
    Watching
}

public static class WatchStatusExtensions
{
    public static string ToStringCached(this WatchStatus status) => status switch
    {
        WatchStatus.PlanToWatch => "Plan To Watch",
        WatchStatus.Completed => "Completed",
        WatchStatus.Dropped => "Dropped",
        WatchStatus.OnHold => "On Hold",
        WatchStatus.Watching => "Watching",
        _ => throw new UnreachableException()
    };
}