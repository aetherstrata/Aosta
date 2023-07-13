using System.Text;
using Aosta.Common;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Query;
using Aosta.Jikan.Query.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Models.Search;

/// <summary>
/// Model class of search configuration for advanced anime search.
/// </summary>
public class AnimeSearchConfig : ISearchConfig
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
	/// Anime type of searched result.
	/// </summary>
	public AnimeTypeFilter Type { get; init; }

	/// <summary>
	/// Minimum score results (1-10).
	/// </summary>
	public int? MinimumScore { get; init; }
		
	/// <summary>
	/// Maximum score results (1-10).
	/// </summary>
	public int? MaximumScore { get; init; }

	/// <summary>
	/// Age rating.
	/// </summary>
	public AnimeAgeRatingFilter Rating { get; init; }

	/// <summary>
	/// Current status.
	/// </summary>
	public AiringStatusFilter Status { get; init; }

	/// <summary>
	/// Select order property.
	/// </summary>
	public AnimeSearchOrderBy OrderBy { get; init; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; init; }

	/// <summary>
	/// Genres to include.
	/// </summary>
	public ICollection<AnimeGenreSearch>? Genres { get; init; } = new List<AnimeGenreSearch>();
		
	/// <summary>
	/// Genres to exclude.
	/// </summary>
	public ICollection<MangaGenreSearch>? ExcludedGenres { get; init; } = new List<MangaGenreSearch>();

	/// <summary>
	/// Filter by producer id.
	/// </summary>
	public ICollection<long>? ProducerIds { get; init; } = new List<long>();

	/// <summary>
	/// Should only search for sfw title. Filter out adult entries.
	/// </summary>
	public bool Sfw { get; init; } = true;

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
			Guard.IsLessOrEqualThan(PageSize.Value,JikanParameterConsts.MaximumPageSize, nameof(PageSize.Value));
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

        Guard.IsValidEnum(Type, nameof(Type));
        builder.Append($"type={Type.GetEnumMemberValue()}&");

		if (MinimumScore.HasValue)
		{
			builder.Append($"min_score={MinimumScore}&");
		}
			
		if (MaximumScore.HasValue)
		{
			builder.Append($"max_score={MaximumScore}&");
		}


        Guard.IsValidEnum(Rating, nameof(Rating));
        builder.Append($"rated={Rating.GetEnumMemberValue()}&");


		if (Status != AiringStatusFilter.EveryStatus)
		{
			Guard.IsValidEnum(Status, nameof(Status));
			builder.Append($"status={Status.GetEnumMemberValue()}&");
		}

		if (Genres?.Count > 0 )
		{
			var genresIds = Genres.Select(genreSearch =>
			{
				Guard.IsValidEnum(genreSearch, nameof(genreSearch));
				return genreSearch.GetEnumMemberValue();
			}).ToArray();

			builder.Append($"genres={string.Join(",", genresIds)}&");
		}
			
			
		if (ExcludedGenres?.Count > 0 )
		{
			string[] genresIds = ExcludedGenres.Select(genreSearch =>
			{
				Guard.IsValidEnum(genreSearch, nameof(genreSearch));
				return genreSearch.GetEnumMemberValue();
			}).ToArray();

			builder.Append($"genre_exclude={string.Join(",", genresIds)}&");
		}

		if (OrderBy != AnimeSearchOrderBy.NoSorting)
		{
			Guard.IsValidEnum(OrderBy, nameof(OrderBy));
			Guard.IsValidEnum(SortDirection, nameof(SortDirection));
			builder.Append($"order_by={OrderBy.GetEnumMemberValue()}&");
			builder.Append($"sort={SortDirection.GetEnumMemberValue()}&");
		}

		if (ProducerIds?.Count > 0)
		{
			builder.Append($"producers={string.Join(",", ProducerIds)}&");
		}

		if (Sfw)
		{
			builder.Append("sfw");
		}

		return builder.ToString().Trim('&');
	}
}