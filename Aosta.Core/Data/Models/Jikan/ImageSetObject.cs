using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using JikanDotNet;
using Realms;

#nullable disable

namespace Aosta.Core.Data.Models.Jikan;

/// <summary> The set of images for a resource. They can come in different sizes and formats. </summary>
public partial class ImageSetObject : IEmbeddedObject
{
    public ImageObject JPG { get; set; }

    public ImageObject WebP { get; set; }

    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private ImageSetObject() { }

    internal ImageSetObject(ImagesSet set)
    {
        JPG = set.JPG?.ToRealmObject();
        WebP = set.WebP?.ToRealmObject();
    }
}
