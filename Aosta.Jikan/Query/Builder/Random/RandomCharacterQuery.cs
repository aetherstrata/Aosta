namespace Aosta.Jikan.Query;

internal static class RandomCharacterQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Characters
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }
}