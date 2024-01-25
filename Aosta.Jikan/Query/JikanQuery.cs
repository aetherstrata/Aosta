using System.Collections;

using Aosta.Common.Extensions;

namespace Aosta.Jikan.Query;

internal sealed class JikanQuery : IQuery, IReadOnlySet<IQueryParameter>
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

    public IEnumerator<IQueryParameter> GetEnumerator()
    {
        return _parameters.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _parameters.Count;
    public bool Contains(IQueryParameter item) => _parameters.Contains(item);

    public bool IsProperSubsetOf(IEnumerable<IQueryParameter> other) => _parameters.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<IQueryParameter> other) => _parameters.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<IQueryParameter> other) => _parameters.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<IQueryParameter> other) => _parameters.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<IQueryParameter> other) => _parameters.Overlaps(other);
    public bool SetEquals(IEnumerable<IQueryParameter> other) => _parameters.SetEquals(other);
}
