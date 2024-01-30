namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterVoiceActorsQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.VOICES
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
