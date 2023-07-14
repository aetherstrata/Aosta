using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class MagazinesQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Magazines
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    public static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(string query)
    {
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Query, query);
    }

    public static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(string query, int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        Guard.IsNotNullOrWhiteSpace(query, nameof(query));
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page)
            .WithParameter(QueryParameter.Query, query);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>> Create(MagazinesQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<MagazineResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}