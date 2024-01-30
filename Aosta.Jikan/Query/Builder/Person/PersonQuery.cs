namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PEOPLE,
        id.ToString()
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
