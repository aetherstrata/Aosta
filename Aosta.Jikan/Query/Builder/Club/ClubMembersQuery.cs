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
        return JikanQuery.Create(getEndpoint(id));
    }

    internal static IQuery Create(long id, int page)
    {
        Guard.IsGreaterThanZero(id, nameof(id));
        Guard.IsGreaterThanZero(page, nameof(page));
        return JikanQuery.Create(getEndpoint(id))
            .With(QueryParameter.PAGE, page);
    }
}
