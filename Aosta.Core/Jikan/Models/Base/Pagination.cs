﻿using System.Text.Json.Serialization;

namespace Aosta.Core.Jikan.Models.Base;

/// <summary>
/// Pagination model
/// </summary>
public class Pagination
{
	/// <summary>
	/// Last visible page
	/// </summary>
	[JsonPropertyName("last_visible_page")]
	public required int LastVisiblePage { get; set; }

	/// <summary>
	/// Has next page
	/// </summary>
	[JsonPropertyName("has_next_page")]
	public required bool HasNextPage { get; set; }
		
	/// <summary>
	/// Current page
	/// </summary>
	[JsonPropertyName("current_page")]
	public required int? CurrentPage { get; set; }
    
	/// <summary>
	/// Summary about current query
	/// </summary>
	[JsonPropertyName("items")]
	public required PaginationSummary Items { get; set; }
}