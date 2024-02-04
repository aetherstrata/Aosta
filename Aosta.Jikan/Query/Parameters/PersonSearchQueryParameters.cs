using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class PersonSearchQueryParameters : JikanQueryParameterSet, IFactory<PersonSearchQueryParameters>
{
    private PersonSearchQueryParameters(){}

    public static PersonSearchQueryParameters Create()
    {
        return new PersonSearchQueryParameters();
    }

    public PersonSearchQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public PersonSearchQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public PersonSearchQueryParameters Query(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public PersonSearchQueryParameters Sort(PersonSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.ORDER_BY, orderBy);
        return this;
    }

    public PersonSearchQueryParameters SortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.SORT, sort);
        return this;
    }

    public PersonSearchQueryParameters Letter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }
}
