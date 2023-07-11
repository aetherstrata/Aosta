using Aosta.Jikan.Consts;
using Aosta.Jikan.Enums;
using Aosta.Utils.Extensions;

namespace Aosta.Jikan.Queries;

internal static class ScheduleQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Schedules
    };

    internal static IQuery Create()
    {
        return new Query(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new Query(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery Create(ScheduledDay day)
    {
        Guard.IsValidEnum(day, nameof(day));
        return new Query(QueryEndpoint)
            .WithParameter(QueryParameter.Filter, day.StringValue());
    }

    internal static IQuery Create(ScheduleQueryParameters parameters)
    {
        return new Query(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}