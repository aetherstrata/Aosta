using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Title model class
/// </summary>
[Preserve(AllMembers = true)]
public partial class TitleEntry : IEmbeddedObject
{
    /// <summary>
    /// Type of title (usually the language).
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Value of the Title.
    /// </summary>
    public string? Title { get; set; }

    public static readonly TitleEntry EMPTY = new()
    {
        Type = string.Empty,
        Title = string.Empty
    };
}
