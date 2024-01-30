namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CLUBS,
        id.ToString()
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
