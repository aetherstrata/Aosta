using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class ProducersQueryParameters : JikanQueryParameterSet
{
    public ProducersQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public ProducersQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public ProducersQueryParameters SetQuery(string query)
    {
        base.Add(QueryParameter.Query, query);
        return this;
    }

    public ProducersQueryParameters OrderBy(ProducerOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        base.Add(QueryParameter.OrderBy, orderBy);
        return this;
    }

    public ProducersQueryParameters Sort(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        base.Add(QueryParameter.Sort, sort);
        return this;
    }

    public ProducersQueryParameters SetLetter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        base.Add(QueryParameter.Letter, letter);
        return this;
    }
}