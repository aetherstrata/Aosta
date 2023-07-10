using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Character;

internal sealed class CharacterVoiceActorsQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Voices
    };

    private CharacterVoiceActorsQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static CharacterVoiceActorsQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new CharacterVoiceActorsQuery(id);
    }
}