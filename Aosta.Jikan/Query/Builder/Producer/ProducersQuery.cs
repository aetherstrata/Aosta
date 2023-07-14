using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query;

internal static class ProducersQuery
{
    private static readonly string[] QueryEndpoint =
    {
        JikanEndpointConsts.Producers
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>>(QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>>(QueryEndpoint)
            .WithParameter(QueryParameter.Page, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>> Create(ProducersQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>>(QueryEndpoint)
            .WithParameterRange(parameters);
    }
}