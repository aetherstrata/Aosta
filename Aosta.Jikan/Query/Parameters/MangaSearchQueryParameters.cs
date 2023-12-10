using System.Text.RegularExpressions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public partial class MangaSearchQueryParameters : JikanQueryParameterSet
{
    [GeneratedRegex(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.Compiled)]
    private static partial Regex dateFormatRegex();

    public MangaSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public MangaSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public MangaSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public MangaSearchQueryParameters SetSafeForWork(bool sfw)
    {
        Add(QueryParameter.SAFE_FOR_WORK, sfw);
        return this;
    }

    public MangaSearchQueryParameters SetUnapproved(bool unapproved)
    {
        Add(QueryParameter.UNAPPROVED, unapproved);
        return this;
    }

    public MangaSearchQueryParameters SetType(MangaTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    public MangaSearchQueryParameters SetScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters SetScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters SetMinScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MIN_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters SetMinScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MIN_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters SetMaxScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MAX_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters SetMaxScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MAX_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters SetStatus(PublishingStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        Add(QueryParameter.STATUS, status);
        return this;
    }

    public MangaSearchQueryParameters SetGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        if (!genreIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        Add(QueryParameter.GENRES, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetExcludedGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        if (!genreIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        Add(QueryParameter.EXCLUDED_GENRES, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetOrder(MangaSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.ORDER_BY, orderBy);
        return this;
    }

    public MangaSearchQueryParameters SetSortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.SORT, sort);
        return this;
    }

    public MangaSearchQueryParameters SetProducers(ICollection<long> producerIds)
    {
        Guard.IsNotNull(producerIds, nameof(producerIds));
        if (!producerIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), producerIds, nameof(producerIds), "All producer IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", producerIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetMagazines(ICollection<long> magazineIds)
    {
        Guard.IsNotNull(magazineIds, nameof(magazineIds));
        if (!magazineIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), magazineIds, nameof(magazineIds), "All magazine IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", magazineIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }

    public MangaSearchQueryParameters SetStartDate(string date)
    {
        Guard.IsValid(s => dateFormatRegex().IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        Add(QueryParameter.START_DATE, date);
        return this;
    }

    public MangaSearchQueryParameters SetEndDate(string date)
    {
        Guard.IsValid(s => dateFormatRegex().IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        Add(QueryParameter.END_DATE, date);
        return this;
    }
}
