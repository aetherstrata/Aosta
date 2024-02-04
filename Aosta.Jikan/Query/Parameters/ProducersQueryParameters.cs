using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class ProducersQueryParameters : JikanQueryParameterSet, IFactory<ProducersQueryParameters>
{
    private ProducersQueryParameters(){}

    public static ProducersQueryParameters Create()
    {
        return new ProducersQueryParameters();
    }

    public ProducersQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public ProducersQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public ProducersQueryParameters Query(string query)
    {
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public ProducersQueryParameters Sort(ProducerOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.ORDER_BY, orderBy);
        return this;
    }

    public ProducersQueryParameters SortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.SORT, sort);
        return this;
    }

    public ProducersQueryParameters Letter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }
}
