using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonAnimeQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.ANIME
    };

    internal static IQuery<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<PersonAnimeographyEntryResponse>>>(getEndpoint(id));
    }
}
