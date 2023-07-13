using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class AnimeRelationsQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.Anime,
        id.ToString(),
        JikanEndpointConsts.Relations
    };

    internal static IQuery<BaseJikanResponse<ICollection<RelatedEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<RelatedEntryResponse>>>(GetEndpoint(id));
    }
}