using System.Text.RegularExpressions;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public partial class AnimeSearchQueryParameters : JikanQueryParameterSet
{
    [GeneratedRegex(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.CultureInvariant, 1)]
    private static partial Regex dateRegex();

    public int Page
    {
        init
        {
            Guard.IsGreaterThanZero(value, nameof(Page));
            Add(QueryParameter.PAGE, value);
        }
    }

    public int Limit
    {
        init
        {
            Guard.IsGreaterThanZero(value, nameof(Limit));
            Guard.IsLessOrEqualThan(value, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(Limit));
            Add(QueryParameter.LIMIT, value);
        }
    }

    public string Query
    {
        init
        {
            Guard.IsNotNullOrWhiteSpace(value, nameof(Query));
            Add(QueryParameter.QUERY, value);
        }
    }

    public bool SFW
    {
        init => Add(QueryParameter.SAFE_FOR_WORK, value);
    }

    public bool Unapproved
    {
        init => Add(QueryParameter.UNAPPROVED, value);
    }

    public AnimeTypeFilter Type
    {
        init
        {
            Guard.IsValidEnum(value, nameof(Type));
            Add(QueryParameter.TYPE, value);
        }
    }

    public double Score
    {
        init
        {
            Guard.IsGreaterThanZero(value, nameof(Score));
            Guard.IsLessOrEqualThan(value, 10, nameof(Score));
            Add(QueryParameter.SCORE, value);
        }
    }

    public double MinScore
    {
        init
        {
            Guard.IsGreaterThanZero(value, nameof(MinScore));
            Guard.IsLessOrEqualThan(value, 10, nameof(MinScore));
            Add(QueryParameter.MIN_SCORE, value);
        }
    }

    public double MaxScore
    {
        init
        {
            Guard.IsGreaterThanZero(value, nameof(MaxScore));
            Guard.IsLessOrEqualThan(value, 10, nameof(MaxScore));
            Add(QueryParameter.MAX_SCORE, value);
        }
    }

    public AiringStatusFilter Status
    {
        init
        {
            Guard.IsValidEnum(value, nameof(Status));
            Add(QueryParameter.STATUS, value);
        }
    }

    public AnimeAgeRatingFilter Rating
    {
        init
        {
            Guard.IsValidEnum(value, nameof(Rating));
            Add(QueryParameter.RATING, value);
        }
    }

    public ICollection<long> Genres
    {
        init
        {
            Guard.IsNotNull(value, nameof(Genres));
            Guard.IsValid(list => list.Any(id => id <= 0), value, nameof(Genres), "All genre IDs must be greater than 0.");
            Add(QueryParameter.GENRES, string.Join(",", value.Select(id => id.ToString())));
        }
    }

    public ICollection<long> ExcludedGenres
    {
        init
        {
            Guard.IsNotNull(value, nameof(ExcludedGenres));
            Guard.IsValid(list => list.Any(id => id <= 0), value, nameof(ExcludedGenres), "All genre IDs must be greater than 0.");
            Add(QueryParameter.EXCLUDED_GENRES, string.Join(",", value.Select(id => id.ToString())));
        }
    }

    public AnimeSearchOrderBy Order
    {
        init
        {
            Guard.IsValidEnum(value, nameof(Order));
            Add(QueryParameter.ORDER_BY, value);
        }
    }

    public SortDirection SortDirection
    {
        init
        {
            Guard.IsValidEnum(value, nameof(SortDirection));
            Add(QueryParameter.SORT, value);
        }
    }

    public ICollection<long> Producers
    {
        init
        {
            Guard.IsNotNull(value, nameof(Producers));
            Guard.IsValid(list => list.Any(id => id <= 0), value, nameof(Producers), "All producer IDs must be greater than 0.");
            Add(QueryParameter.PRODUCERS, string.Join(",", value.Select(id => id.ToString())));
        }
    }

    public char Letter
    {
        init
        {
            Guard.IsLetter(value, nameof(Letter));
            Add(QueryParameter.LETTER, value);
        }
    }

    //[Obsolete("Change this to a DateOnly")]
    public string StartDate
    {
        init
        {
            Guard.IsValid(s => dateRegex().IsMatch(s), value, nameof(StartDate), "This date is not in a valid format.");
            Add(QueryParameter.START_DATE, value);
        }
    }

    public string EndDate
    {
        init
        {
            Guard.IsValid(s => dateRegex().IsMatch(s), value, nameof(EndDate), "This date is not in a valid format.");
            Add(QueryParameter.END_DATE, value);
        }
    }
}
