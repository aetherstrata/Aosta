namespace Aosta.Jikan.Query.Builder.Club;

internal static class ClubMembersQuery
{
    private static string[] getEndpoint(long id) =>
    [
        JikanEndpointConsts.CLUBS,
        id.ToString(),
        JikanEndpointConsts.MEMBERS
    ];

    internal static IQuery Create(long id)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        return new JikanQuery(getEndpoint(id));
    }

    internal static IQuery Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(getEndpoint(id))
            .Add(QueryParameter.PAGE, page);
    }
}
