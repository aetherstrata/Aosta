using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonMangaQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.MANGA
    };

    internal static IQuery<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<PersonMangaographyEntryResponse>>>(getEndpoint(id));
    }
}
