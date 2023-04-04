using Aosta.Core.Jikan.Models.Response;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary>Image URL model class.</summary>
public partial class ImageObject : IEmbeddedObject
{
    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private ImageObject()
    {
    }

    internal ImageObject(ImageResponse imageResponse)
    {
        ImageUrl = imageResponse.ImageUrl ?? string.Empty;
        SmallImageUrl = imageResponse.SmallImageUrl ?? string.Empty;
        MediumImageUrl = imageResponse.MediumImageUrl ?? string.Empty;
        LargeImageUrl = imageResponse.LargeImageUrl ?? string.Empty;
        MaximumImageUrl = imageResponse.MaximumImageUrl ?? string.Empty;
    }

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
}