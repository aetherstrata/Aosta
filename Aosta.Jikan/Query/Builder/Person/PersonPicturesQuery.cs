using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Person;

internal static class PersonPicturesQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.PICTURES
    };

    internal static IQuery<BaseJikanResponse<ICollection<ImagesSetResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ImagesSetResponse>>>(getEndpoint(id));
    }
}
