using JikanDotNet;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary>Title model class</summary>
public partial class TitleObject : IEmbeddedObject
{
    /// <summary>Type of title (usually the language).</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>Value of the Title.</summary>
    public string Title { get; set; } = string.Empty;

    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private TitleObject () { }

    internal TitleObject(TitleEntry title)
    {
        Type = title.Type ?? string.Empty;
        Title = title.Title ?? string.Empty;
    }
}