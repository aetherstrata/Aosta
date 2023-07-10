using Aosta.Jikan.Consts;
using Aosta.Jikan.Query.Person;

namespace Aosta.Jikan.Query.Schedule;

internal sealed class ScheduleQuery : Query
{
    private static readonly string[] QueryEndpoint = new []
    {
        JikanEndpointConsts.Schedules
    };

    private ScheduleQuery() : base(QueryEndpoint)
    {
    }

    private ScheduleQuery(IQueryParameterList parameters) : this()
    {
        Parameters.AddRange(parameters.GetParameters());
    }

    internal static ScheduleQuery Create()
    {
        return new ScheduleQuery();
    }

    internal static ScheduleQuery Create(ScheduleQueryParameters parameters)
    {
        return new ScheduleQuery(parameters);
    }
}