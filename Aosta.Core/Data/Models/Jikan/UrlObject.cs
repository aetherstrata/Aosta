using Aosta.Core.Jikan.Models.Response;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

public partial class UrlObject : IEmbeddedObject
{
    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private UrlObject()
    {
    }

    internal UrlObject(MalUrlResponse urlResponse)
    {
        MalId = urlResponse.MalId;
        Type = urlResponse.Type ?? string.Empty;
        Url = urlResponse.Url ?? string.Empty;
        Name = urlResponse.Name ?? string.Empty;
    }

    /// <summary>ID associated with MyAnimeList.</summary>
    public long MalId { get; set; }

    /// <summary>Type of resource</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>Url to sub item main page.</summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>Title/Name of the item</summary>
    public string Name { get; set; } = string.Empty;
}