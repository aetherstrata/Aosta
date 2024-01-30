namespace Aosta.Jikan.Query.Builder.Season;

internal static class SeasonArchiveQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.SEASONS
    ];

    internal static IQuery Create()
    {
        return new JikanQuery(s_QueryEndpoint);
    }
}
