using FastEnumUtility;

namespace Aosta.Core.Database.Enums;

public enum AiringStatus
{
    NotAvailable = -1,

    Cancelled,

    [Label("Finished Airing")]
    FinishedAiring,

    [Label("Not yet aired")]
    NotYetAired,

    [Label("Currently Airing")]
    CurrentlyAiring
}
