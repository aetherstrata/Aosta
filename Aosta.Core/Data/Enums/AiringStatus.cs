using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

public enum AiringStatus
{
    NotAvailable = -1,
    Cancelled,
    NotYetAired,
    FinishedAiring,
    CurrentlyAiring
}

internal static class AiringStatusExtensions
{
    internal static string ToStringCached(this AiringStatus status) => status switch
    {
        AiringStatus.NotAvailable => "Not Available",
        AiringStatus.CurrentlyAiring => "Currently Airing",
        AiringStatus.FinishedAiring => "Finished Airing",
        AiringStatus.NotYetAired => "Not Yet Aired",
        AiringStatus.Cancelled => "Cancelled",
        _ => throw new UnreachableException()
    };
}