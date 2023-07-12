namespace Aosta.Jikan.Query;

internal static class AnimeQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString()
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}