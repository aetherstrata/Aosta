using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class CharacterAnimeQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Characters,
        id.ToString(),
        JikanEndpointConsts.Anime
    };
    
    internal static IQuery<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<CharacterAnimeographyEntryResponse>>>(GetEndpoint(id));
    }
}