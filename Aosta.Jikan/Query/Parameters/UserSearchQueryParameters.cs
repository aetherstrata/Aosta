using Aosta.Jikan.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class UserSearchQueryParameters : JikanQueryParameterSet
{
    public UserSearchQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public UserSearchQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public UserSearchQueryParameters SetQuery(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        base.Add(QueryParameter.Query, query);
        return this;
    }

    public UserSearchQueryParameters SetGender(UserGenderFilter gender)
    {
        Guard.IsValidEnum(gender, nameof(gender));
        base.Add(QueryParameter.Gender, gender);
        return this;
    }

    public UserSearchQueryParameters SetLocation(string location)
    {
        Guard.IsNotNullOrWhiteSpace(location, nameof(location));
        base.Add(QueryParameter.Query, location);
        return this;
    }

    public UserSearchQueryParameters SetMinimumAge(int age)
    {
        Guard.IsGreaterThanZero(age, nameof(age));
        base.Add(QueryParameter.MinAge, age);
        return this;
    }

    public UserSearchQueryParameters SetMaximumAge(int age)
    {
        Guard.IsGreaterThanZero(age, nameof(age));
        base.Add(QueryParameter.MaxAge, age);
        return this;
    }
}