using Aosta.Jikan.Enums;
using Aosta.Common.Extensions;

namespace Aosta.Jikan.Query;

internal static class ScheduleQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Schedules
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery Create(ScheduledDay day)
    {
        Guard.IsValidEnum(day, nameof(day));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, day.StringValue());
    }

    internal static IQuery Create(ScheduleQueryParameters parameters)
    {
        return new JikanQuery(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}