using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class PersonVoiceActingRolesQuery
{
    private static string[] getEndpoint(long id) => new []
    {
        JikanEndpointConsts.PEOPLE,
        id.ToString(),
        JikanEndpointConsts.VOICES
    };

    internal static IQuery<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>>(getEndpoint(id));
    }
}
