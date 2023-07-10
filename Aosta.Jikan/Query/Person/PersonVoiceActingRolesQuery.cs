using Aosta.Jikan.Consts;

namespace Aosta.Jikan.Query.Person;

internal sealed class PersonVoiceActingRolesQuery : Query
{
    private static string[] QueryEndpoint(long id) => new []
    {
        JikanEndpointConsts.People,
        id.ToString(),
        JikanEndpointConsts.Voices
    };

    private PersonVoiceActingRolesQuery(long id) : base(QueryEndpoint(id))
    {
    }

    internal static PersonVoiceActingRolesQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new PersonVoiceActingRolesQuery(id);
    }
}