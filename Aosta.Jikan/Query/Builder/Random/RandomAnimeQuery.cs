namespace Aosta.Jikan.Query;

internal static class RandomAnimeQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Anime
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}