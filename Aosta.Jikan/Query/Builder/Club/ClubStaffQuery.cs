namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubStaffQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CLUBS,
        id.ToString(),
        JikanEndpointConsts.STAFF
    ];

    internal static IQuery Crete(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
