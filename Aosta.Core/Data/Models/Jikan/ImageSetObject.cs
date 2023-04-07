using Aosta.Jikan.Models.Response;
using Realms;

namespace Aosta.Core.Data.Models.Jikan;

/// <summary> The set of images for a resource. They can come in different sizes and formats. </summary>
public partial class ImageSetObject : IEmbeddedObject
{
    /// <remarks>The parameterless constructor is made private because this model should only be created from a Jikan response.</remarks>
    private ImageSetObject() { }

    internal ImageSetObject(ImagesSetResponse setResponse)
    {
        JPG = setResponse?.JPG?.ToRealmObject();
        WebP = setResponse?.WebP?.ToRealmObject();
    }

    public ImageObject? JPG { get; set; }

    public ImageObject? WebP { get; set; }
}