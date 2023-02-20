using System.Text.Json.Serialization;
using Realms;

namespace Aosta.Core.Data.Realm;

/// <summary>Image URL model class.</summary>
public class OnlineImageObject : RealmObject
{
    public static readonly OnlineImageObject Empty = new();

    [PrimaryKey]
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Url to default version of the image.</summary>
    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>Url to small version of the image.</summary>
    [JsonPropertyName("small_image_url")]
    public string SmallImageUrl { get; set; } = string.Empty;

    /// <summary>Url to medium version of the image.</summary>
    [JsonPropertyName("medium_image_url")]
    public string MediumImageUrl { get; set; } = string.Empty;

    /// <summary>Url to large version of the image.</summary>
    [JsonPropertyName("large_image_url")]
    public string LargeImageUrl { get; set; } = string.Empty;

    /// <summary>Url to version of image with the biggest resolution.</summary>
    [JsonPropertyName("maximum_image_url")]
    public string MaximumImageUrl { get; set; } = string.Empty;
}