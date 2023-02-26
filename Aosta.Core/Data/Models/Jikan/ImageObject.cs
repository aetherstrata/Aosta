using JikanDotNet;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary>Image URL model class.</summary>
public partial class ImageObject : IEmbeddedObject
{
    /// <summary>Url to default version of the image.</summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>Url to small version of the image.</summary>
    public string SmallImageUrl { get; set; } = string.Empty;

    /// <summary>Url to medium version of the image.</summary>
    public string MediumImageUrl { get; set; } = string.Empty;

    /// <summary>Url to large version of the image.</summary>
    public string LargeImageUrl { get; set; } = string.Empty;

    /// <summary>Url to version of image with the biggest resolution.</summary>
    public string MaximumImageUrl { get; set; } = string.Empty;

    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private ImageObject() { }

    internal ImageObject(Image image)
    {
        ImageUrl = image.ImageUrl ?? string.Empty;
        SmallImageUrl = image.SmallImageUrl ?? string.Empty;
        MediumImageUrl = image.MediumImageUrl ?? string.Empty;
        LargeImageUrl = image.LargeImageUrl ?? string.Empty;
        MaximumImageUrl = image.MaximumImageUrl ?? string.Empty;
    }
}