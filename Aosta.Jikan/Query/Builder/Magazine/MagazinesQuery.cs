namespace Aosta.Jikan.Query;

internal static class MagazinesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Magazines
    };

    internal static IQuery Create()
    {
        return new JikanQuery(QueryEndpoint);
    }

    internal static IQuery Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }
}