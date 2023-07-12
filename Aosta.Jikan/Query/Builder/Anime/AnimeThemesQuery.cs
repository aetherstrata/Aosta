namespace Aosta.Jikan.Query;

internal static class AnimeThemesQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Themes
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}