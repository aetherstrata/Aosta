using System.Text.RegularExpressions;
using Aosta.Common.Extensions;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class AnimeSearchQueryParameters : JikanQueryParameterSet
{
    private static Regex regex = new(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.Compiled);

    public AnimeSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public AnimeSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public AnimeSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        base.Add(QueryParameter.Query, query);
        return this;
    }

    public AnimeSearchQueryParameters SetSafeForWork(bool sfw)
    {
        base.Add(QueryParameter.SafeForWork, sfw);
        return this;
    }

    public AnimeSearchQueryParameters SetUnapproved(bool unapproved)
    {
        base.Add(QueryParameter.Unapproved, unapproved);
        return this;
    }

    public AnimeSearchQueryParameters SetType(AnimeTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        base.Add(QueryParameter.Type, type);
        return this;
    }

    public AnimeSearchQueryParameters SetScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.Score, score);
        return this;
    }

    public AnimeSearchQueryParameters SetScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.Score, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMinScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MinScore, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMinScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MinScore, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMaxScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MaxScore, score);
        return this;
    }

    public AnimeSearchQueryParameters SetMaxScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MaxScore, score);
        return this;
    }

    public AnimeSearchQueryParameters SetStatus(AiringStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        base.Add(QueryParameter.Status, status);
        return this;
    }

    public AnimeSearchQueryParameters SetRating(AnimeAgeRatingFilter rating)
    {
        Guard.IsValidEnum(rating, nameof(rating));
        base.Add(QueryParameter.Rating, rating);
        return this;
    }

    public AnimeSearchQueryParameters SetGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        base.Add(QueryParameter.Genres, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetGenres(long genreId)
    {
        Guard.IsGreaterThanZero(genreId, nameof(genreId));
        base.Add(QueryParameter.Genres, genreId);
        return this;
    }

    public AnimeSearchQueryParameters SetExcludedGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        base.Add(QueryParameter.ExcludedGenres, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetExcludedGenres(long genreId)
    {
        Guard.IsGreaterThanZero(genreId, nameof(genreId));
        base.Add(QueryParameter.ExcludedGenres, genreId);
        return this;
    }

    public AnimeSearchQueryParameters SetOrder(AnimeSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        base.Add(QueryParameter.OrderBy, orderBy);
        return this;
    }

    public AnimeSearchQueryParameters SetSortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        base.Add(QueryParameter.Sort, sort);
        return this;
    }

    public AnimeSearchQueryParameters SetProducers(ICollection<long> producerIds)
    {
        Guard.IsNotNull(producerIds, nameof(producerIds));
        Guard.IsValid(list => list.Any(id => id <= 0), producerIds, nameof(producerIds), "All producer IDs must be greater than 0.");
        base.Add(QueryParameter.Producers, string.Join(",", producerIds.Select(id => id.ToString())));
        return this;
    }

    public AnimeSearchQueryParameters SetProducers(long producerId)
    {
        Guard.IsGreaterThanZero(producerId, nameof(producerId));
        base.Add(QueryParameter.Producers, producerId);
        return this;
    }

    public AnimeSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        base.Add(QueryParameter.Letter, letter);
        return this;
    }

    public AnimeSearchQueryParameters SetStartDate(string date)
    {
        Guard.IsValid(s => regex.IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        base.Add(QueryParameter.StartDate, date);
        return this;
    }

    public AnimeSearchQueryParameters SetEndDate(string date)
    {
        Guard.IsValid(s => regex.IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        base.Add(QueryParameter.EndDate, date);
        return this;
    }
}