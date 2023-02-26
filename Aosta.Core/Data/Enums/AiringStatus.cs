using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

public enum AiringStatus
{
    NotAvailable = -1,
    CurrentlyAiring,
    FinishedAiring,
    Cancelled,
    OnHold,
    Announced,
    OnPause
}

internal static class AiringStatusExtensions
{
    internal static string ToStringCached(this AiringStatus status) => status switch
    {
        AiringStatus.NotAvailable => "Not Available",
        AiringStatus.CurrentlyAiring => "Currently Airing",
        AiringStatus.FinishedAiring => "Finished Airing",
        AiringStatus.Announced => "Announced",
        AiringStatus.Cancelled => "Cancelled",
        AiringStatus.OnHold => "On Hold",
        AiringStatus.OnPause => "On Pause",
        _ => throw new UnreachableException()
    };
}