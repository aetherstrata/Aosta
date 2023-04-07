using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Response;

/// <summary>
/// Producer with additional data model class.
/// </summary>
public class ProducerResponseFull: ProducerResponse
{
    /// <summary>
    /// Producer's external links
    /// </summary>
    [JsonPropertyName("external")]
    public ICollection<ExternalLinkResponse>? External { get; set; }
}