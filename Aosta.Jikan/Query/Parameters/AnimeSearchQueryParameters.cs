using System.Text.RegularExpressions;
using Aosta.Jikan.Enums;
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

    public AnimeSearchQueryParameters SetSafeForWork(bool sfw)
    {
        Add(QueryParameter.SAFE_FOR_WORK, sfw);
        return this;
    }

    public AnimeSearchQueryParameters SetUnapproved(bool unapproved)
    {
        Add(QueryParameter.UNAPPROVED, unapproved);
        return this;
    }

    public AnimeSearchQueryParameters SetType(AnimeTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    public AnimeSearchQueryParameters SetScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters SetScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMinScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MIN_SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMinScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MIN_SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMaxScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MAX_SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMaxScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MAX_SCORE, score);
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

    public AnimeSearchQueryParameters SetGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        Add(QueryParameter.GENRES, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetGenres(long genreId)
    {
        Guard.IsGreaterThanZero(genreId, nameof(genreId));
        Add(QueryParameter.GENRES, genreId);
        return this;
    }

    public AnimeSearchQueryParameters SetExcludedGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        Add(QueryParameter.EXCLUDED_GENRES, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetExcludedGenres(long genreId)
    {
        Guard.IsGreaterThanZero(genreId, nameof(genreId));
        Add(QueryParameter.EXCLUDED_GENRES, genreId);
        return this;
    }

    public AnimeSearchQueryParameters SetOrder(AnimeSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.ORDER_BY, orderBy);
        return this;
    }

    public AnimeSearchQueryParameters SetSortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.SORT, sort);
        return this;
    }

    public AnimeSearchQueryParameters SetProducers(ICollection<long> producerIds)
    {
        Guard.IsNotNull(producerIds, nameof(producerIds));
        Guard.IsValid(list => list.Any(id => id <= 0), producerIds, nameof(producerIds), "All producer IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", producerIds.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetProducers(long producerId)
    {
        Guard.IsGreaterThanZero(producerId, nameof(producerId));
        Add(QueryParameter.PRODUCERS, producerId);
        return this;
    }

    public AnimeSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }

    public AnimeSearchQueryParameters SetStartDate(string date)
    {
        Guard.IsValid(s => dateRegex().IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        Add(QueryParameter.START_DATE, date);
        return this;
    }

    public AnimeSearchQueryParameters SetEndDate(string date)
    {
        Guard.IsValid(s => dateRegex().IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        Add(QueryParameter.END_DATE, date);
        return this;
    }
}
