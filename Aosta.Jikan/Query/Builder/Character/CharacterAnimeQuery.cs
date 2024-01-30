namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterAnimeQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.ANIME
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
