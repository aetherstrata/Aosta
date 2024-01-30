using System.Diagnostics.CodeAnalysis;

using Realms;

namespace Aosta.Data.Database.Models.Embedded;

/// <summary>
/// Title model class
/// </summary>
[Preserve(AllMembers = true)]
public partial class TitleEntry : IEmbeddedObject
{
    /// <summary>
    /// Type of title (usually the language).
    /// </summary>
    public required string Type { get; set; } = null!;

    /// <summary>
    /// Value of the Title.
    /// </summary>
    public required string Title { get; set; } = null!;

    [SetsRequiredMembers]
    public TitleEntry(string type, string title)
    {
        Type = type;
        Title = title;
    }

    // Parameterless constructor required for Realm
    [SetsRequiredMembers]
    private TitleEntry()
    {
    }
}
