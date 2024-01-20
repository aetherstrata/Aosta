using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterFullDataQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.FULL
    };

    internal static IQuery<BaseJikanResponse<CharacterResponseFull>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<CharacterResponseFull>>(getEndpoint(id));
    }
}
