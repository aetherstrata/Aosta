using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CHARACTERS,
        id.ToString()
    };

    internal static IQuery<BaseJikanResponse<CharacterResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<CharacterResponse>>(getEndpoint(id));
    }
}
