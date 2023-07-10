using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Query.Schedule;

public class ScheduleQueryParameters : IQueryParameterList
{
    public int? Page { get; init; }

    public int? Limit { get; init; }

    public ScheduledDay? Filter { get; init; }

    public bool Kids { get; init; }

    public bool SafeForWork { get; init; }

    public bool Unapproved { get; init; }

    IEnumerable<IQueryParameter> IQueryParameterList.GetParameters()
    {
        var list = new List<IQueryParameter>();

        if (Page.HasValue)
        {
            Guard.IsGreaterThanZero(Page.Value, nameof(Page.Value));
            list.Add(new QueryParameter<int>()
            {
                Name = QueryParameter.Page,
                Value = Page.Value
            });
        }

        if (Limit.HasValue)
        {
            Guard.IsGreaterThanZero(Limit.Value, nameof(Limit.Value));
            Guard.IsLessOrEqualThan(Limit.Value, ParameterConsts.MaximumPageSize, nameof(Limit.Value));
            list.Add(new QueryParameter<int>()
            {
                Name = QueryParameter.Limit,
                Value = Limit.Value
            });
        }

        if (Filter.HasValue)
        {
            Guard.IsValidEnum(Filter.Value, nameof(Filter.Value));
            list.Add(new QueryParameter<string>()
            {
                Name = QueryParameter.Filter,
                Value = Filter.Value.GetEnumMemberValue()
            });
        }

        if (Kids)
        {
            list.Add(new QueryParameter<string>()
            {
                Name = QueryParameter.Kids,
                Value = Kids.ToString().ToLower()
            });
        }

        if (SafeForWork)
        {
            list.Add(new QueryParameter<bool>()
            {
                Name = QueryParameter.SafeForWork,
                Value = SafeForWork
            });
        }

        if (Unapproved)
        {
            list.Add(new QueryParameter<bool>()
            {
                Name = QueryParameter.Unapproved,
                Value = Unapproved
            });
        }

        return list;
    }
}