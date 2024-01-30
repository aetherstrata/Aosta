namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterMangaQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.MANGA
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }
}
