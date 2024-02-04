namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CHARACTERS,
        id.ToString()
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return JikanQuery.Create(getEndpoint(id));
    }
}
