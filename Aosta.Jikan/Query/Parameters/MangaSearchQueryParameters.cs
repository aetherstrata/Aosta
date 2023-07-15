using System.Text.RegularExpressions;
using Aosta.Common.Extensions;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public partial class MangaSearchQueryParameters : JikanQueryParameterSet
{
    [GeneratedRegex(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.Compiled)]
    private static partial Regex DateFormatRegex();
    
    public MangaSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.Page, page);
        return this;
    }

    public MangaSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        Add(QueryParameter.Limit, limit);
        return this;
    }

    public MangaSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.Query, query);
        return this;
    }

    public MangaSearchQueryParameters SetSafeForWork(bool sfw)
    {
        Add(QueryParameter.SafeForWork, sfw);
        return this;
    }

    public MangaSearchQueryParameters SetUnapproved(bool unapproved)
    {
        Add(QueryParameter.Unapproved, unapproved);
        return this;
    }

    public MangaSearchQueryParameters SetType(MangaTypeFilter type)
    {
        Guard.IsValidEnum(type, nameof(type));
        Add(QueryParameter.Type, type);
        return this;
    }

    public MangaSearchQueryParameters SetScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.Score, score);
        return this;
    }

    public MangaSearchQueryParameters SetScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.Score, score);
        return this;
    }

    public MangaSearchQueryParameters SetMinScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MinScore, score);
        return this;
    }

    public MangaSearchQueryParameters SetMinScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MinScore, score);
        return this;
    }

    public MangaSearchQueryParameters SetMaxScore(int score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MaxScore, score);
        return this;
    }

    public MangaSearchQueryParameters SetMaxScore(double score)
    {
        Guard.IsGreaterThanZero(score, nameof(score));
        Guard.IsLessOrEqualThan(score, 10, nameof(score));
        Add(QueryParameter.MaxScore, score);
        return this;
    }

    public MangaSearchQueryParameters SetStatus(PublishingStatusFilter status)
    {
        Guard.IsValidEnum(status, nameof(status));
        Add(QueryParameter.Status, status);
        return this;
    }

    public MangaSearchQueryParameters SetGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        if (!genreIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        Add(QueryParameter.Genres, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetExcludedGenres(ICollection<long> genreIds)
    {
        Guard.IsNotNull(genreIds, nameof(genreIds));
        if (!genreIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), genreIds, nameof(genreIds), "All genre IDs must be greater than 0.");
        Add(QueryParameter.ExcludedGenres, string.Join(",", genreIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetOrder(MangaSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.OrderBy, orderBy);
        return this;
    }

    public MangaSearchQueryParameters SetSortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.Sort, sort);
        return this;
    }

    public MangaSearchQueryParameters SetProducers(ICollection<long> producerIds)
    {
        Guard.IsNotNull(producerIds, nameof(producerIds));
        if (!producerIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), producerIds, nameof(producerIds), "All producer IDs must be greater than 0.");
        Add(QueryParameter.Producers, string.Join(",", producerIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetMagazines(ICollection<long> magazineIds)
    {
        Guard.IsNotNull(magazineIds, nameof(magazineIds));
        if (!magazineIds.Any()) return this; // Do not add the parameter if there are no ids in the collection
        Guard.IsValid(list => list.Any(id => id <= 0), magazineIds, nameof(magazineIds), "All magazine IDs must be greater than 0.");
        Add(QueryParameter.Producers, string.Join(",", magazineIds.Select(id => id.ToString())));
        return this;
    }

    public MangaSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.Letter, letter);
        return this;
    }

    public MangaSearchQueryParameters SetStartDate(string date)
    {
        Guard.IsValid(s => DateFormatRegex().IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        Add(QueryParameter.StartDate, date);
        return this;
    }

    public MangaSearchQueryParameters SetEndDate(string date)
    {
        Guard.IsValid(s => DateFormatRegex().IsMatch(s), date, nameof(date), "This date is not in a valid format.");
        Add(QueryParameter.EndDate, date);
        return this;
    }
}