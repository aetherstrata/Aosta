namespace Aosta.Jikan.Query;

internal static class CharacterPicturesQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Pictures
    };
    
    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(GetEndpoint(id));
    }
}