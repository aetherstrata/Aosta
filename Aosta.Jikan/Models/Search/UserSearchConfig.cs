using System.Text;
using Aosta.Core.Utils;
using Aosta.Core.Utils.Extensions;
using Aosta.Jikan.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Models.Search;

/// <summary>
/// Model class of search configuration for advanced user search.
/// </summary>
public class UserSearchConfig: ISearchConfig
{
    /// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; init; }
    
    /// <summary>
	/// Search query.
	/// </summary>
	public string? Query { get; init; }
	
	/// <summary>
	/// Gender of the user.
	/// </summary>
	public UserGender Gender { get; init; }
	
	/// <summary>
	/// Location of the searched users.
	/// </summary>
	public string? Location { get; init; }
	
	/// <summary>
	/// Max age of the searched users.
	/// </summary>
	public int? MaxAge { get; init; }
	
	/// <summary>
	/// Min age of the searched users.
	/// </summary>
	public int? MinAge { get; init; }
	
    /// <summary>
    /// Create query from current parameters for search request.
    /// </summary>
    /// <returns>Query from current parameters for search request</returns>
    public string ConfigToString()
    {
    	var builder = new StringBuilder();

        if (Page.HasValue)
        {
	        Guard.IsGreaterThanZero(Page.Value, nameof(Page.Value));
	        builder.Append($"page={Page.Value}&");
        }
        
        if (!string.IsNullOrWhiteSpace(Query))
        {
	        builder.Append($"q={Query}&");
        }
        
        if (Gender != UserGender.Any)
        {
	        Guard.IsValidEnum(Gender, nameof(Gender));
	        builder.Append($"gender={Gender.GetEnumMemberValue()}&");
        }
        
        if (!string.IsNullOrWhiteSpace(Location))
        {
	        builder.Append($"location={Location}&");
        }
        
        if (MinAge.HasValue)
        {
	        builder.Append($"minAge={MinAge.Value}&");
        }
        
        if (MaxAge.HasValue)
        {
	        builder.Append($"maxAge={MaxAge.Value}&");
        }

    	return builder.Length == 0 ? string.Empty : builder.Prepend("?").ToString().Trim('&');;
    }
}