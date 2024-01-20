using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Manga;

internal static class MangaRelationsQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.MANGA,
        id.ToString(),
        JikanEndpointConsts.RELATIONS
    };

    internal static IQuery<BaseJikanResponse<ICollection<RelatedEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<RelatedEntryResponse>>>(getEndpoint(id));
    }
}
