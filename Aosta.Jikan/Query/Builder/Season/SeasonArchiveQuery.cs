namespace Aosta.Jikan.Query;

internal static class SeasonArchiveQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Seasons
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}