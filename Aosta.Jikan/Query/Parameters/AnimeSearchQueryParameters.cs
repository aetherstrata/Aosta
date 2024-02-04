using System.Text.RegularExpressions;

using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public partial class AnimeSearchQueryParameters : JikanQueryParameterSet
{
    [GeneratedRegex(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.CultureInvariant, 1)]
    private static partial Regex dateRegex();

    public AnimeSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public AnimeSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public AnimeSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public AnimeSearchQueryParameters SetSfw(bool sfw)
    {
        //TODO: Seems like jikan changed how they handle sfw.
        //INFO: https://github.com/jikan-me/jikan-rest/issues/486
        Add(QueryParameter.SAFE_FOR_WORK, sfw.ToStringLower());
        return this;
    }

    public AnimeSearchQueryParameters SetUnapproved(bool approved)
    {
        Add(QueryParameter.UNAPPROVED, approved);
        return this;
    }

    public AnimeSearchQueryParameters SetType(AnimeTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(Type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    public AnimeSearchQueryParameters SetScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMinScore(double minScore)
    {
        Guard.IsGreaterThanZero(minScore, nameof(minScore));
        Guard.IsLessOrEqualThan(minScore, 10, nameof(minScore));
        Add(QueryParameter.MIN_SCORE, minScore);
        return this;
    }

    public AnimeSearchQueryParameters SetMaxScore(double maxScore)
    {
        Guard.IsGreaterThanZero(maxScore, nameof(maxScore));
        Guard.IsLessOrEqualThan(maxScore, 10, nameof(maxScore));
        Add(QueryParameter.MAX_SCORE, maxScore);
        return this;
    }

    public AnimeSearchQueryParameters SetStatus(AiringStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        Add(QueryParameter.STATUS, status);
        return this;
    }

    public AnimeSearchQueryParameters SetRating(AnimeAgeRatingFilter rating)
    {
        Guard.IsValidEnum(rating, nameof(rating));
        Add(QueryParameter.RATING, rating);
        return this;
    }

    public AnimeSearchQueryParameters SetGenres(ICollection<long> genres)
    {
        Guard.IsNotNull(genres, nameof(genres));
        Guard.IsValid(list => list.Any(id => id <= 0),
            genres, nameof(genres),
            "All genre IDs must be greater than 0.");
        Add(QueryParameter.GENRES, string.Join(",", genres.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetExcludedGenres(ICollection<long> excludedGenres)
    {
        Guard.IsNotNull(excludedGenres, nameof(excludedGenres));
        Guard.IsValid(list => list.Any(id => id <= 0),
            excludedGenres, nameof(excludedGenres),
            "All genre IDs must be greater than 0.");
        Add(QueryParameter.EXCLUDED_GENRES, string.Join(",", excludedGenres.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetSort(AnimeSearchOrderBy order)
    {
        Guard.IsValidEnum(order, nameof(order));
        Add(QueryParameter.ORDER_BY, order);
        return this;
    }

    public AnimeSearchQueryParameters SetSortDirection(SortDirection sortDirection)
    {
        Guard.IsValidEnum(sortDirection, nameof(sortDirection));
        Add(QueryParameter.SORT, sortDirection);
        return this;
    }

    public AnimeSearchQueryParameters SetProducers(ICollection<long> producers)
    {
        Guard.IsNotNull(producers, nameof(producers));
        Guard.IsValid(list => list.Any(id => id <= 0),
            producers, nameof(producers),
            "All producer IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", producers.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }

    //[Obsolete("Change this to a DateOnly")]
    public AnimeSearchQueryParameters SetStartDate(string startDate)
    {
        Guard.IsValid(s => dateRegex().IsMatch(s),
            startDate, nameof(startDate),
            "This date is not in a valid format.");
        Add(QueryParameter.START_DATE, startDate);
        return this;
    }

    public AnimeSearchQueryParameters SetEndDate(string endDate)
    {
        Guard.IsValid(s => dateRegex().IsMatch(s),
            endDate, nameof(endDate),
            "This date is not in a valid format.");
        Add(QueryParameter.END_DATE, endDate);
        return this;
    }
}
