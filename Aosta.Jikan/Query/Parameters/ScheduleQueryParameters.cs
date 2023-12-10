using Aosta.Jikan.Query.Enums;

namespace Aosta.Jikan.Query.Parameters;

public class ScheduleQueryParameters : JikanQueryParameterSet
{
    public ScheduleQueryParameters SetPage(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        base.Add(QueryParameter.Page, page);
        return this;
    }

    public ScheduleQueryParameters SetLimit(int limit)
    {
        Guard.IsGreaterThanZero(limit, nameof(limit));
        Guard.IsLessOrEqualThan(limit, JikanParameterConsts.MaximumPageSize, nameof(limit));
        base.Add(QueryParameter.Limit, limit);
        return this;
    }

    public ScheduleQueryParameters SetFilter(ScheduledDayFilter filter)
    {
        Guard.IsValidEnum(filter, nameof(filter));
        base.Add(QueryParameter.Filter, filter);
        return this;
    }

    public ScheduleQueryParameters SetKids(bool kids)
    {
        base.Add(QueryParameter.Kids, kids);
        return this;
    }

    public ScheduleQueryParameters SetSafeForWork(bool sfw)
    {
        base.Add(QueryParameter.SafeForWork, sfw);
        return this;
    }

    public ScheduleQueryParameters SetUnapproved(bool unapproved)
    {
        base.Add(QueryParameter.Unapproved, unapproved);
        return this;
    }
}