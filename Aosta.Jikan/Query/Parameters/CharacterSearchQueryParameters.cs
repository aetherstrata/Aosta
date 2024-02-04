using Aosta.Common;
using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class CharacterSearchQueryParameters : JikanQueryParameterSet, IFactory<CharacterSearchQueryParameters>
{
    private CharacterSearchQueryParameters(){}

    public static CharacterSearchQueryParameters Create()
    {
        return new CharacterSearchQueryParameters();
    }

    public CharacterSearchQueryParameters Page(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public CharacterSearchQueryParameters Limit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public CharacterSearchQueryParameters Query(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public CharacterSearchQueryParameters Order(CharacterSearchOrderBy orderBy)
    {
        Guard.IsValidEnum(orderBy, nameof(orderBy));
        Add(QueryParameter.ORDER_BY, orderBy);
        return this;
    }

    public CharacterSearchQueryParameters SortDirection(SortDirection sort)
    {
        Guard.IsValidEnum(sort, nameof(sort));
        Add(QueryParameter.SORT, sort);
        return this;
    }

    public CharacterSearchQueryParameters Letter(char letter)
    {
        Guard.IsLetter(letter, nameof(letter));
        Add(QueryParameter.LETTER, letter);
        return this;
    }
}
