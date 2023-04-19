using Realms;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Model for external links for manga/anime
/// </summary>
public partial class ExternalLink : IEmbeddedObject
{
    /// <summary>
    /// Name of the external service.
    /// </summary>
    public string? Name { get; init; }
    
    /// <summary>
    /// Url to external service.
    /// </summary>
    public string? Url { get; init; }
}
