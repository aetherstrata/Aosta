using System.Diagnostics;

namespace Aosta.Core.Data.Enums;

public enum AiringStatus
{
    NotAvailable = -1,
    Cancelled,
    FinishedAiring,
    NotYetAired,
    CurrentlyAiring
}

internal static class AiringStatusExtensions
{
    internal static string ToStringCached(this AiringStatus status) => status switch
    {
        AiringStatus.NotAvailable => "Not available",
        AiringStatus.Cancelled => "Cancelled",
        AiringStatus.FinishedAiring => "Finished airing",
        AiringStatus.NotYetAired => "Not yet aired",
        AiringStatus.CurrentlyAiring => "Currently airing",
        _ => throw new UnreachableException()
    };
}