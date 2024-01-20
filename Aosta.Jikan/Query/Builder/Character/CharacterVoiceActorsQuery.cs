using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterVoiceActorsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.VOICES
    };

    internal static IQuery<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<VoiceActorEntryResponse>>>(getEndpoint(id));
    }
}
