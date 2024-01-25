using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Character;

internal static class CharacterSearchQuery
{
    private static readonly string[] s_QueryEndpoint =
    [
        JikanEndpointConsts.CHARACTERS
    ];

    internal static IQuery Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery(s_QueryEndpoint)
            .Add(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(CharacterSearchQueryParameters parameters)
    {
        return new JikanQuery(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
