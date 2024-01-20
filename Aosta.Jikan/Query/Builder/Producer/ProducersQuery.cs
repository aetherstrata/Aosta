using Aosta.Jikan.Models.Base;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

namespace Aosta.Jikan.Query.Builder.Producer;

internal static class ProducersQuery
{
    private static readonly string[] s_QueryEndpoint =
    {
        JikanEndpointConsts.PRODUCERS
    };

    internal static IQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>> Create()
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>>(s_QueryEndpoint);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>> Create(int page)
    {
        Guard.IsGreaterThanZero(page, nameof(page));
        return new JikanQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>>(s_QueryEndpoint)
            .WithParameter(QueryParameter.PAGE, page);
    }

    internal static IQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>> Create(ProducersQueryParameters parameters)
    {
        return new JikanQuery<PaginatedJikanResponse<ICollection<ProducerResponse>>>(s_QueryEndpoint)
            .WithParameterRange(parameters);
    }
}
