namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonVoiceActingRolesQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.VOICES
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
