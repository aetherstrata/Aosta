using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class CharacterQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString()
    };

    internal static IQuery<BaseJikanResponse<CharacterResponse>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<CharacterResponse>>(GetEndpoint(id));
    }
}