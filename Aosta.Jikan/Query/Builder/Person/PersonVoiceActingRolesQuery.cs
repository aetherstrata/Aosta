using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan.Query;

internal static class PersonVoiceActingRolesQuery
{
    private static string[] GetEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Voices
    };

    internal static IQuery<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>> Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery<BaseJikanResponse<ICollection<VoiceActingRoleResponse>>>(GetEndpoint(id));
    }
}