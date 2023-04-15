using System.Text;
using Aosta.Core.Utils;
using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Models.Search;

/// <summary>
/// Model class of search configuration for advanced person search.
/// </summary>
public class ClubSearchConfig: ISearchConfig
{
    /// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; init; }
	
	/// <summary>
	/// Size of the page (25 is the max).
	/// </summary>
	public int? PageSize { get; init; }
	
	/// <summary>
	/// Search query.
	/// </summary>
	public string? Query { get; init; }
	
	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; init; }
	
	/// <summary>
	/// Filter by club category.
	/// </summary>
	public ClubCategory Category { get; init; }
	
	/// <summary>
	/// Filter by club category.
	/// </summary>
	public ClubType Type { get; init; }

	/// <summary>
	/// Select order by property.
	/// </summary>
	public ClubSearchOrderBy OrderBy { get; init; }

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

        if (Page.HasValue)
        {
	        Guard.IsGreaterThanZero(Page.Value, nameof(Page.Value));
	        builder.Append($"page={Page.Value}&");
        }
        
        if (PageSize.HasValue)
        {
	        Guard.IsGreaterThanZero(PageSize.Value, nameof(PageSize.Value));
	        Guard.IsLessOrEqualThan(PageSize.Value,ParameterConsts.MaximumPageSize, nameof(PageSize.Value));
	        builder.Append($"limit={PageSize.Value}&");
        }
        
        if (!string.IsNullOrWhiteSpace(Query))
        {
	        builder.Append($"q={Query}&");
        }
        
        if (Letter.HasValue)
        {
	        Guard.IsLetter(Letter.Value, nameof(Letter.Value));
	        builder.Append($"letter={Letter.Value}&");
        }
        
        if (Category != ClubCategory.EveryCategory)
        {
	        Guard.IsValidEnum(Category, nameof(Category));
	        builder.Append($"category={Category}&");
        }
        
        if (Type != ClubType.EveryType)
        {
	        Guard.IsValidEnum(Type, nameof(Type));
	        builder.Append($"type={Type}&");
        }
        
    	if (OrderBy != ClubSearchOrderBy.NoSorting)
    	{
    		Guard.IsValidEnum(OrderBy, nameof(OrderBy));
    		Guard.IsValidEnum(SortDirection, nameof(SortDirection));
    		builder.Append($"order_by={OrderBy.GetEnumMemberValue()}&");
    		builder.Append($"sort={SortDirection.GetEnumMemberValue()}");
    	}

    	return builder.ToString().Trim('&');
    }
}