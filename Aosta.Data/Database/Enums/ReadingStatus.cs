using FastEnumUtility;

namespace Aosta.Data.Database.Enums;

public enum ReadingStatus
{
    [Label("Plan to read")]
    PlanToRead,

    [Label("Dropped")]
    Dropped,

    [Label("Completed")]
    Completed,

    [Label("On hold")]
    OnHold,

    [Label("Reading")]
    Reading
}