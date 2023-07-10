using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Character;

internal sealed class CharacterPicturesQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Pictures
    };

    private CharacterPicturesQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static CharacterPicturesQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new CharacterPicturesQuery(id);
    }
}