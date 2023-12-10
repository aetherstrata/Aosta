using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class CharacterMangaQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.MANGA
    };

    internal static IQuery<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<CharacterMangaographyEntryResponse>>>(getEndpoint(id));
    }
}
