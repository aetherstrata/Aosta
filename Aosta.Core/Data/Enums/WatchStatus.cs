using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

public enum WatchStatus
{
    PlanToWatch = -1,
    Dropped,
    Completed,
    OnHold,
    Watching
}

public static class WatchStatusExtensions
{
    public static string ToStringCached(this WatchStatus status)
    {
        return status switch
        {
            WatchStatus.PlanToWatch => "Plan to watch",
            WatchStatus.Dropped => "Dropped",
            WatchStatus.Completed => "Completed",
            WatchStatus.OnHold => "On Hold",
            WatchStatus.Watching => "Watching",
            _ => throw new UnreachableException()
        };
    }
}