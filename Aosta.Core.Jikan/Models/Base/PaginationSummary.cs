using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Base;

/// <summary>
/// Pagination sumamry model (about current query)
/// </summary>
public class PaginationSummary
{
    /// <summary>
    /// Count of items returned
    /// </summary>
    [JsonPropertyName("count")]
    public required int Count { get; set; }
    
    /// <summary>
    /// Total items count.
    /// </summary>
    [JsonPropertyName("total")]
    public required int Total { get; set; }
    
    /// <summary>
    /// Count of items in the single page
    /// </summary>
    [JsonPropertyName("per_page")]
    public required int PerPage { get; set; }
}