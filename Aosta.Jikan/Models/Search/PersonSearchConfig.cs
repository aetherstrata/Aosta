using System.Text;
using Aosta.Common;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Query;
using FastEnumUtility;

namespace Aosta.Jikan.Models.Search;

/// <summary>
/// Model class of search configuration for advanced person search.
/// </summary>
public class PersonSearchConfig: ISearchConfig
{
	/// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int Page { get; init; }
	
	/// <summary>
	/// Size of the page (25 is the max).
	/// </summary>
	public int PageSize { get; init; }
	
	/// <summary>
	/// Search query.
	/// </summary>
	public string? Query { get; init; }
	
	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; init; }

	/// <summary>
	/// Select order by property.
	/// </summary>
	public PersonSearchOrderBy OrderBy { get; init; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; init; }
	
    /// <summary>
    /// Create query from current parameters for search request.
    /// </summary>
    /// <returns>Query from current parameters for search request</returns>
    public string ConfigToString()
    {
    	var builder = new StringBuilder().Append('?');

        Guard.IsGreaterThanZero(Page, nameof(Page));
        builder.Append($"page={Page}&");

		Guard.IsGreaterThanZero(PageSize, nameof(PageSize));
        Guard.IsLessOrEqualThan(PageSize,ParameterConsts.MaximumPageSize, nameof(PageSize));
        builder.Append($"limit={PageSize}&");
        
        if (!string.IsNullOrWhiteSpace(Query))
        {
	        builder.Append($"q={Query}&");
        }
        
        if (Letter.HasValue)
        {
	        Guard.IsLetter(Letter.Value, nameof(Letter.Value));
	        builder.Append($"letter={Letter.Value}&");
        }
    	
    	if (OrderBy != PersonSearchOrderBy.NoSorting)
    	{
    		Guard.IsValidEnum(OrderBy, nameof(OrderBy));
    		Guard.IsValidEnum(SortDirection, nameof(SortDirection));
    		builder.Append($"order_by={OrderBy.GetEnumMemberValue()}&");
    		builder.Append($"sort={SortDirection.GetEnumMemberValue()}");
    	}

    	return builder.ToString().Trim('&');
    }
}