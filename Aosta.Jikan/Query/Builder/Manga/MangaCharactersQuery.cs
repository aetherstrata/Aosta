using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class MangaCharactersQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Manga,
        id.ToString(),
        JikanEndpointConsts.Characters
    };

    internal static IQuery<BaseJikanResponse<ICollection<MangaCharacterResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<MangaCharacterResponse>>>(GetEndpoint(id));
    }
}