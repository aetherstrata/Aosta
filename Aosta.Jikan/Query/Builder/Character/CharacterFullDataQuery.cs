namespace Aosta.Jikan.Query;

internal static class CharacterFullDataQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Full
    };

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}