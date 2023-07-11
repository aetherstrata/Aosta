using Aosta.Utils.Extensions;

namespace Aosta.Jikan.Queries;

internal sealed class Query : IQuery
{
    /// <summary>
    /// Query endpoint
    /// </summary>
    private readonly string _endpoint;

    /// <summary>
    /// Query parameters
    /// </summary>
    private readonly QueryParameterSet _parameters = new();

    /// <summary>
    /// Initialize the query using endpoint route.
    /// </summary>
    /// <param name="routeSections">Route sections to be joined to form the endpoint url.</param>
    internal Query(IEnumerable<string> routeSections)
    {
        _endpoint = string.Join("/", routeSections);
    }

    /// <summary>
    /// Adds a query parameter to the query
    /// </summary>
    /// <param name="name">Parameter name</param>
    /// <param name="value">Parameter value</param>
    /// <typeparam name="T">Parameter type</typeparam>
    /// <returns>The updated query</returns>
    internal Query WithParameter<T>(string name, T value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <summary>
    /// Adds a range of <see cref="IQueryParameter"/> to the query
    /// </summary>
    /// <param name="parameters">Range of query parameters</param>
    /// <returns>The updated query</returns>
    internal Query WithParameterRange(QueryParameterSet parameters)
    {
        parameters.ForEach(_parameters.Add);
        return this;
    }

    /// <summary>
    /// Formulates and returns the complete query string.
    /// </summary>
    /// <returns>The complete query string.</returns>
    string IQuery.GetQuery()
    {
        return _endpoint + _parameters;
    }
}
