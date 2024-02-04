using Aosta.Common;
using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class MangaSearchQueryParameters : JikanQueryParameterSet, IFactory<MangaSearchQueryParameters>
{
    private MangaSearchQueryParameters(){}

    public static MangaSearchQueryParameters Create()
    {
        return new MangaSearchQueryParameters();
    }

    public MangaSearchQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public MangaSearchQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public MangaSearchQueryParameters Query(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public MangaSearchQueryParameters SafeForWork(bool sfw)
    {
        //TODO: Seems like jikan changed how they handle sfw.
        //INFO: https://github.com/jikan-me/jikan-rest/issues/486
        Add(QueryParameter.SAFE_FOR_WORK, sfw.ToStringLower());
        return this;
    }

    public MangaSearchQueryParameters Unapproved(bool unapproved)
    {
        Add(QueryParameter.UNAPPROVED, unapproved);
        return this;
    }

    public MangaSearchQueryParameters Type(MangaTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    public MangaSearchQueryParameters Score(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters Score(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters MinScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MIN_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters MinScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MIN_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters MaxScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MAX_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters MaxScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MAX_SCORE, score);
        return this;
    }

    public MangaSearchQueryParameters Status(PublishingStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        Add(QueryParameter.STATUS, status);
        return this;
    }

    public MangaSearchQueryParameters Genres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        if (genreIds.Count == 0) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            genreIds, nameof(genreIds),
            "All genre IDs must be greater than 0.");
        Add(QueryParameter.GENRES, string.Join(",", genreIds.Select(static id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters ExcludedGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        if (genreIds.Count == 0) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            genreIds, nameof(genreIds),
            "All genre IDs must be greater than 0.");
        Add(QueryParameter.EXCLUDED_GENRES, string.Join(",", genreIds.Select(static id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters Order(MangaSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.ORDER_BY, orderBy);
        return this;
    }

    public MangaSearchQueryParameters SortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.SORT, sort);
        return this;
    }

    public MangaSearchQueryParameters Producers(ICollection<long> producerIds)
    {
        Guard.IsNotNull(producerIds, nameof(producerIds));
        if (producerIds.Count == 0) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            producerIds, nameof(producerIds),
            "All producer IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", producerIds.Select(static id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters Magazines(ICollection<long> magazineIds)
    {
        Guard.IsNotNull(magazineIds, nameof(magazineIds));
        if (magazineIds.Count == 0) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            magazineIds, nameof(magazineIds),
            "All magazine IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", magazineIds.Select(static id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters Letter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }

    public MangaSearchQueryParameters StartDate(string date)
    {
        Guard.IsValid(static s => DateRegex().IsMatch(s),
            date, nameof(date),
            "This date is not in a valid format.");
        Add(QueryParameter.START_DATE, date);
        return this;
    }

    public MangaSearchQueryParameters EndDate(string date)
    {
        Guard.IsValid(static s => DateRegex().IsMatch(s),
            date, nameof(date),
            "This date is not in a valid format.");
        Add(QueryParameter.END_DATE, date);
        return this;
    }
}
