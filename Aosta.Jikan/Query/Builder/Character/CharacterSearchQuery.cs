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
        return JikanQuery.Create(s_QueryEndpoint)
            .With(QueryParameter.QUERY, query);
    }

    internal static IQuery Create(CharacterSearchQueryParameters parameters)
    {
        return JikanQuery.Create(s_QueryEndpoint)
            .AddRange(parameters);
    }
}
