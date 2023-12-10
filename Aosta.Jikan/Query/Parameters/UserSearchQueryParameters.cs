using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class UserSearchQueryParameters : JikanQueryParameterSet
{
    public UserSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Add(QueryParameter.PAGE, page);
        return this;
    }

    public UserSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MAXIMUM_PAGE_SIZE, nameof(limit));
        Add(QueryParameter.LIMIT, limit);
        return this;
    }

    public UserSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        Add(QueryParameter.QUERY, query);
        return this;
    }

    public UserSearchQueryParameters SetGender(UserGenderFilter gender)
    {
        Guard.IsValidEnum(gender, nameof(gender));
        Add(QueryParameter.GENDER, gender);
        return this;
    }

    public UserSearchQueryParameters SetLocation(string location)
    {
        Guard.IsNotNullOrWhiteSpace(location, nameof(location));
        Add(QueryParameter.QUERY, location);
        return this;
    }

    public UserSearchQueryParameters SetMinimumAge(int age)
    {
        Guard.IsGreaterThanZero(age, nameof(age));
        Add(QueryParameter.MIN_AGE, age);
        return this;
    }

    public UserSearchQueryParameters SetMaximumAge(int age)
    {
        Guard.IsGreaterThanZero(age, nameof(age));
        Add(QueryParameter.MAX_AGE, age);
        return this;
    }
}
