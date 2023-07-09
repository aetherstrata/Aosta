using FastEnumUtility;

namespace Aosta.Core.Database.Enums;

public enum AiringStatus
{
    [Label("Not available")]
    NotAvailable,

    [Label("Cancelled")]
    Cancelled,

    [Label("Finished Airing")]
    FinishedAiring,

    [Label("Not yet aired")]
    NotYetAired,

    [Label("Currently Airing")]
    CurrentlyAiring
}
