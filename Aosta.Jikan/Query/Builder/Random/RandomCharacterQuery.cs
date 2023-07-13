using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class RandomCharacterQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Random,
        JikanEndpointConsts.Characters
    };

    internal static IQuery<BaseJikanResponse<CharacterResponse>> Create()
    {
        return new JikanQuery<BaseJikanResponse<CharacterResponse>>(QueryEndpoint);
    }
}