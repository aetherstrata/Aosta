using Aosta.Core.Extensions;
using JikanDotNet;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary> The set of images for a resource. They can come in different sizes and formats. </summary>
public partial class ImageSetObject : IEmbeddedObject
{
    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private ImageSetObject() { }

    internal ImageSetObject(ImagesSet set)
    {
        JPG = set?.JPG?.ToRealmObject();
        WebP = set?.WebP?.ToRealmObject();
    }

    public ImageObject? JPG { get; set; }

    public ImageObject? WebP { get; set; }
}