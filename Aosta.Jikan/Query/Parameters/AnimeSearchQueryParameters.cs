using Aosta.Common;
using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class AnimeSearchQueryParameters : JikanQueryParameterSet, IFactory<AnimeSearchQueryParameters>
{
    private AnimeSearchQueryParameters(){}

    public static AnimeSearchQueryParameters Create()
    {
        return new AnimeSearchQueryParameters();
    }

    public AnimeSearchQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public AnimeSearchQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public AnimeSearchQueryParameters Query(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public AnimeSearchQueryParameters SafeForWork(bool sfw)
    {
        //TODO: Seems like jikan changed how they handle sfw.
        //INFO: https://github.com/jikan-me/jikan-rest/issues/486
        Add(QueryParameter.SAFE_FOR_WORK, sfw.ToStringLower());
        return this;
    }

    public AnimeSearchQueryParameters Unapproved(bool approved)
    {
        Add(QueryParameter.UNAPPROVED, approved);
        return this;
    }

    public AnimeSearchQueryParameters Type(AnimeTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(System.Type));
        Add(QueryParameter.TYPE, type);
        return this;
    }

    public AnimeSearchQueryParameters Score(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.SCORE, score);
        return this;
    }

    public AnimeSearchQueryParameters MinScore(double minScore)
    {
        Guard.IsGreaterThanZero(minScore, nameof(minScore));
        Guard.IsLessOrEqualThan(minScore, 10, nameof(minScore));
        Add(QueryParameter.MIN_SCORE, minScore);
        return this;
    }

    public AnimeSearchQueryParameters MaxScore(double maxScore)
    {
        Guard.IsGreaterThanZero(maxScore, nameof(maxScore));
        Guard.IsLessOrEqualThan(maxScore, 10, nameof(maxScore));
        Add(QueryParameter.MAX_SCORE, maxScore);
        return this;
    }

    public AnimeSearchQueryParameters Status(AiringStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        Add(QueryParameter.STATUS, status);
        return this;
    }

    public AnimeSearchQueryParameters Rating(AnimeAgeRatingFilter rating)
    {
        Guard.IsValidEnum(rating, nameof(rating));
        Add(QueryParameter.RATING, rating);
        return this;
    }

    public AnimeSearchQueryParameters Genres(ICollection<long> genres)
    {
        Guard.IsNotNull(genres, nameof(genres));
        if (genres.Count == 0) return this;
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            genres, nameof(genres),
            "All genre IDs must be greater than 0.");
        Add(QueryParameter.GENRES, string.Join(",", genres.Select(static id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters ExcludedGenres(ICollection<long> excludedGenres)
    {
        Guard.IsNotNull(excludedGenres, nameof(excludedGenres));
        if (excludedGenres.Count == 0) return this;
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            excludedGenres, nameof(excludedGenres),
            "All genre IDs must be greater than 0.");
        Add(QueryParameter.EXCLUDED_GENRES, string.Join(",", excludedGenres.Select(static id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters Sort(AnimeSearchOrderBy order)
    {
        Guard.IsValidEnum(order, nameof(order));
        Add(QueryParameter.ORDER_BY, order);
        return this;
    }

    public AnimeSearchQueryParameters SortDirection(SortDirection sortDirection)
    {
        Guard.IsValidEnum(sortDirection, nameof(sortDirection));
        Add(QueryParameter.SORT, sortDirection);
        return this;
    }

    public AnimeSearchQueryParameters Producers(ICollection<long> producers)
    {
        Guard.IsNotNull(producers, nameof(producers));
        if (producers.Count == 0) return this;
        Guard.IsValid(static list => list.Any(static id => id <= 0),
            producers, nameof(producers),
            "All producer IDs must be greater than 0.");
        Add(QueryParameter.PRODUCERS, string.Join(",", producers.Select(static id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters Letter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }

    public AnimeSearchQueryParameters StartDate(string startDate)
    {
        Guard.IsValid(static s => DateRegex().IsMatch(s),
            startDate, nameof(startDate),
            "This date is not in a valid format.");
        Add(QueryParameter.START_DATE, startDate);
        return this;
    }

    public AnimeSearchQueryParameters EndDate(string endDate)
    {
        Guard.IsValid(static s => DateRegex().IsMatch(s),
            endDate, nameof(endDate),
            "This date is not in a valid format.");
        Add(QueryParameter.END_DATE, endDate);
        return this;
    }
}
