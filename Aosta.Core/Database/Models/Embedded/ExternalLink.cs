using Realms;
using System.Runtime.CompilerServices;

namespace Aosta.Core.Database.Models.Embedded;

/// <summary>
/// Model for external links for manga/anime
/// </summary>
[Preserve(AllMembers = true)]
public partial class ExternalLink : IEmbeddedObject
{
    /// <summary>
    /// Name of the external service.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Url to external service.
    /// </summary>
    public string? Url { get; set; }
}
