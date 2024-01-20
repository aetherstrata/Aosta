using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Magazine;

internal static class MagazinesQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.MAGAZINES
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }

    public static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.QUERY, query);
    }

    public static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(string query, int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page)
            .WithParameter(QueryParameter.QUERY, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(MagazinesQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
