using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterPicturesQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.CHARACTERS,
        id.ToString(),
        JikanEndpointConsts.PICTURES
    };

    internal static IQuery<BaseJikanResponse<ICollection<ImagesSetResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<ImagesSetResponse>>>(getEndpoint(id));
    }
}
