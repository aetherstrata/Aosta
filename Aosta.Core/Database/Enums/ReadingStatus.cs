using FastEnumUtility;

namespace Aosta.Core.Database.Enums;

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