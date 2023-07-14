using System.Text.RegularExpressions;
using Aosta.Common.Extensions;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class MangaSearchQueryParameters : JikanQueryParameterSet
{
    private static Regex regex = new(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.Compiled);

    public MangaSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public MangaSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public MangaSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        base.Add(QueryParameter.Query, query);
        return this;
    }

    public MangaSearchQueryParameters SetSafeForWork(bool sfw)
    {
        base.Add(QueryParameter.SafeForWork, sfw);
        return this;
    }

    public MangaSearchQueryParameters SetUnapproved(bool unapproved)
    {
        base.Add(QueryParameter.Unapproved, unapproved);
        return this;
    }

    public MangaSearchQueryParameters SetType(MangaTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        base.Add(QueryParameter.Type, type.StringValue());
        return this;
    }

    public MangaSearchQueryParameters SetScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.Score, score);
        return this;
    }

    public MangaSearchQueryParameters SetScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.Score, Math.Round(score, 2, MidpointRounding.AwayFromZero));
        return this;
    }

    public MangaSearchQueryParameters SetMinScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MinScore, score);
        return this;
    }

    public MangaSearchQueryParameters SetMinScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MinScore, Math.Round(score, 2, MidpointRounding.AwayFromZero));
        return this;
    }

    public MangaSearchQueryParameters SetMaxScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MaxScore, score);
        return this;
    }

    public MangaSearchQueryParameters SetMaxScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        base.Add(QueryParameter.MaxScore, Math.Round(score, 2, MidpointRounding.AwayFromZero));
        return this;
    }

    public MangaSearchQueryParameters SetStatus(PublishingStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        base.Add(QueryParameter.Status, status.StringValue());
        return this;
    }

    public MangaSearchQueryParameters SetGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        base.Add(QueryParameter.Genres, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetExcludedGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        base.Add(QueryParameter.ExcludedGenres, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetOrder(MangaSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        base.Add(QueryParameter.OrderBy, orderBy.StringValue());
        return this;
    }

    public MangaSearchQueryParameters SetSortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        base.Add(QueryParameter.Sort, sort.StringValue());
        return this;
    }

    public MangaSearchQueryParameters SetProducers(ICollection<long> producerIds)
    {
        Guard.IsNotNull(producerIds, nameof(producerIds));
        Guard.IsValid(list => list.Any(id => id <= 0), producerIds, nameof(producerIds), "All producer IDs must be greater than 0.");
        base.Add(QueryParameter.Producers, string.Join(",", producerIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetMagazines(ICollection<long> magazineIds)
    {
        Guard.IsNotNull(magazineIds, nameof(magazineIds));
        Guard.IsValid(list => list.Any(id => id <= 0), magazineIds, nameof(magazineIds), "All magazine IDs must be greater than 0.");
        base.Add(QueryParameter.Producers, string.Join(",", magazineIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        base.Add(QueryParameter.Letter, letter);
        return this;
    }

    public MangaSearchQueryParameters SetStartDate(string date)
    {
        Guard.IsValid(s => regex.IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        base.Add(QueryParameter.StartDate, date);
        return this;
    }

    public MangaSearchQueryParameters SetEndDate(string date)
    {
        Guard.IsValid(s => regex.IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        base.Add(QueryParameter.EndDate, date);
        return this;
    }
}