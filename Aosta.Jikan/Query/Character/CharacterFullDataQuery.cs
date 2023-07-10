using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Character;

internal sealed class CharacterFullDataQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Full
    };

    private CharacterFullDataQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static CharacterFullDataQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new CharacterFullDataQuery(id);
    }
}