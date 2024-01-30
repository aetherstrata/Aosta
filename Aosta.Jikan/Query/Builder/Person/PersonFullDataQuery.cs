namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonFullDataQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.FULL
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
