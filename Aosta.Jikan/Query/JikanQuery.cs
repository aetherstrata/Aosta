namespace Aosta.Jikan.Query;

internal sealed class JikanQuery : IQuery
{
    /// Query endpoint
    private readonly string _endpoint;

    /// Query parameters
    private readonly JikanQueryParameterSet _parameters = new();

    private JikanQuery(string[] routeSections)
    {
        _endpoint = string.Join("/", routeSections);
    }

    /// <summary>
    /// Initialize the query using endpoint route.
    /// </summary>
    /// <param name="routeSections">Route sections to be joined to form the endpoint url.</param>
    internal static JikanQuery Create(string[] routeSections)
    {
        return new JikanQuery(routeSections);
    }

    /// <summary>
    /// Adds a query parameter to the query
    /// </summary>
    /// <param name="name">Parameter name</param>
    /// <param name="value">Parameter value</param>
    /// <typeparam name="TParam">Enum type</typeparam>
    /// <returns>The updated query</returns>
    internal JikanQuery With<TParam>(string name, TParam value) where TParam : struct, Enum
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
    internal JikanQuery With(string name, bool value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <inheritdoc cref="With(string,bool)"/>
    internal JikanQuery With(string name, string value)
    {
        _parameters.Add(name, value);
        return this;
    }

    /// <inheritdoc cref="With(string,bool)"/>
    internal JikanQuery With(string name, int value)
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
        _parameters.AddRange(parameters);
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
