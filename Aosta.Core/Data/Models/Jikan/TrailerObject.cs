using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary>
///     Model class of trailer data for a content
/// </summary>
public partial class TrailerObject : IEmbeddedObject
{
    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private TrailerObject() { }

    internal TrailerObject(AnimeTrailerResponse trailerResponse)
    {
        YoutubeId = trailerResponse.YoutubeId ?? string.Empty;
        Url = trailerResponse.Url ?? string.Empty;
        EmbedUrl = trailerResponse.EmbedUrl ?? string.Empty;
        Image = trailerResponse.Image?.ToRealmObject();
    }

    /// <summary>YouTube id of the trailer. </summary>
    public string YoutubeId { get; set; } = string.Empty;

    /// <summary>YouTube Url of the trailer. </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>YouTube embed Url of the trailer. </summary>
    public string EmbedUrl { get; set; } = string.Empty;

    /// <summary>Thumbnail of the trailer. </summary>
    public ImageObject? Image { get; set; }
}