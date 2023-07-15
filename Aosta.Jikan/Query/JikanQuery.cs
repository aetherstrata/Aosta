using Aosta.Common.Extensions;

namespace Aosta.Jikan.Query;

internal sealed class JikanQuery<T> : IQuery<T>
{
    /// <summary>
    /// Query endpoint
    /// </summary>
    private readonly string _endpoint;

    /// <summary>
    /// Query parameters
    /// </summary>
    private readonly JikanQueryParameterSet _parameters = new();

    /// <summary>
    /// Initialize the query using endpoint route.
    /// </summary>
    /// <param name="routeSections">Route sections to be joined to form the endpoint url.</param>
    internal JikanQuery(IEnumerable<string> routeSections)
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
    internal JikanQuery<T> WithParameter<TParam>(string name, TParam value) where TParam : struct, Enum
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
    internal JikanQuery<T> WithParameter(string name, bool value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <inheritdoc cref="WithParameter(string,bool)"/>
    internal JikanQuery<T> WithParameter(string name, string value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <inheritdoc cref="WithParameter(string,bool)"/>
    internal JikanQuery<T> WithParameter(string name, int value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <summary>
    /// Adds a range of <see cref="IQueryParameter"/> to the query
    /// </summary>
    /// <param name="parameters">Range of query parameters</param>
    /// <returns>The updated query</returns>
    internal JikanQuery<T> WithParameterRange(JikanQueryParameterSet parameters)
    {
        parameters.ForEach(_parameters.Add);
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
}