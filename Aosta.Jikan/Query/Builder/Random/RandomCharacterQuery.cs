using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Random;

internal static class RandomCharacterQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.RANDOM,
        JikanEndpointConsts.CHARACTERS
    };

    internal static IQuery<BaseJikanResponse<CharacterResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<CharacterResponse>>(s_QueryEndpoint);
    }
}
