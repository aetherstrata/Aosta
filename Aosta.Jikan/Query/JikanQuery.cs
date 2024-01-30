using System.Collections;

namespace Aosta.Jikan.Query;

internal sealed class JikanQuery : IQuery, IEnumerable
{
    /// <summary>
    /// Query endpoint
    /// </summary>
    private readonly string _endpoint;

    /// <summary>
    /// Query parameters
    /// </summary>
    private readonly JikanQueryParameterSet _parameters = [];

    /// <summary>
    /// Initialize the query using endpoint route.
    /// </summary>
    /// <param name="routeSections">Route sections to be joined to form the endpoint url.</param>
    internal JikanQuery(string[] routeSections)
    {
        _endpoint = string.Join("/", routeSections);
    }

    /// <summary>
    /// Adds a query parameter to the query
    /// </summary>
    /// <param name="name">Parameter name</param>
    /// <param name="value">Parameter value</param>
    /// <typeparam name="TParam">Enum type</typeparam>
    /// <returns>The updated query</returns>
    internal JikanQuery Add<TParam>(string name, TParam value) where TParam : struct, Enum
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <summary>
    /// Adds a query parameter to the query
    /// </summary>
    /// <param name="name">Parameter name</param>
    /// <param name="value">Parameter value</param>
    /// <returns>The updated query</returns>
    internal JikanQuery Add(string name, bool value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <inheritdoc cref="Add(string,bool)"/>
    internal JikanQuery Add(string name, string value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <inheritdoc cref="Add(string,bool)"/>
    internal JikanQuery Add(string name, int value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <summary>
    /// Adds a range of <see cref="IQueryParameter"/> to the query
    /// </summary>
    /// <param name="parameters">Range of query parameters</param>
    /// <returns>The updated query</returns>
    internal JikanQuery AddRange(JikanQueryParameterSet parameters)
    {
        foreach (var param in parameters)
        {
            _parameters.TryAdd(param.Key, param.Value);
        }

        return this;
    }

    /// <summary>
    /// Formulates and returns the complete query string.
    /// </summary>
    /// <returns>The complete query string.</returns>
    public string GetQuery()
    {
        return _endpoint + _parameters;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _parameters.GetEnumerator();
    }
}
