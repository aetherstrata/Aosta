using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class MagazinesQueryParameters : JikanQueryParameterSet
{
    public MagazinesQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public MagazinesQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public MagazinesQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        base.Add(QueryParameter.Query, query);
        return this;
    }

    public MagazinesQueryParameters OrderBy(MagazinesOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        base.Add(QueryParameter.OrderBy, orderBy);
        return this;
    }

    public MagazinesQueryParameters Sort(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        base.Add(QueryParameter.Sort, sort);
        return this;
    }

    public MagazinesQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        base.Add(QueryParameter.Letter, letter);
        return this;
    }
}