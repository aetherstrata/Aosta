using Aosta.Jikan.Enums;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class ClubSearchQueryParameters : JikanQueryParameterSet
{
    public ClubSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public ClubSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public ClubSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        base.Add(QueryParameter.Query, query);
        return this;
    }

    public ClubSearchQueryParameters SetOrder(ClubSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        base.Add(QueryParameter.OrderBy, orderBy);
        return this;
    }

    public ClubSearchQueryParameters SetSortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        base.Add(QueryParameter.Sort, sort);
        return this;
    }

    public ClubSearchQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        base.Add(QueryParameter.Letter, letter);
        return this;
    }
}