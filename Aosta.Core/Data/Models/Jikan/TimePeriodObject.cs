using JikanDotNet;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary>
///     Model class of content airing period.
/// </summary>
public partial class TimePeriodObject : IEmbeddedObject
{
    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private TimePeriodObject()
    {
    }

    internal TimePeriodObject(TimePeriod period)
    {
        From = period.From;
        To = period.To;
    }

    private DateTimeOffset? _From { get; set; }

    /// <summary> The date the content started airing. </summary>
    public DateTime? From
    {
        get => _From?.Date;
        set => _From = value.HasValue ? new DateTimeOffset(value.Value) : null;
    }

    private DateTimeOffset? _To { get; set; }

    /// <summary> The date the content finished airing. </summary>
    public DateTime? To
    {
        get => _To?.Date;
        set => _To = value.HasValue ? new DateTimeOffset(value.Value) : null;
    }
}