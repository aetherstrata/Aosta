using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Character;

internal sealed class CharacterQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString()
    };

    private CharacterQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static CharacterQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new CharacterQuery(id);
    }
}