using System.Collections;

using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Query;

public class JikanQueryParameterSet : IReadOnlySet<IQueryParameter>
{
    private readonly HashSet<IQueryParameter> _parameters = new(QueryParameter.NameComparer);

    internal JikanQueryParameterSet()
    {
    }

    internal void Add(IQueryParameter parameter)
    {
        if (!_parameters.Add(parameter))
        {
            throw new JikanDuplicateParameterException($"A query parameter named {parameter.GetName()} already exists.", nameof(parameter));
        }
    }

    internal void Add<T>(string name, T value) where T : struct, Enum
    {
        Add(new EnumQueryParameter<T>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, double value)
    {
        Add(new JikanQueryParameter<double>()
        {
            Name = name,
            Value = value
        });
    }


    internal void Add(string name, long value)
    {
        Add(new JikanQueryParameter<long>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, int value)
    {
        Add(new JikanQueryParameter<int>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, string value)
    {
        Add(new JikanQueryParameter<string>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, char value) => Add(name, value.ToString());

    internal void Add(string name, bool value)
    {
        Add(new BoolQueryParameter
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name) => Add(name, true);

    public override string ToString()
    {
        string parameterString = string.Join("&", _parameters
            .Select(x => x.ToString())
            .Where(s => !string.IsNullOrEmpty(s)));

        return string.IsNullOrEmpty(parameterString)
            ? string.Empty
            : "?" + parameterString;
    }

    /// <inheritdoc />
    public IEnumerator<IQueryParameter> GetEnumerator() => _parameters.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public int Count => _parameters.Count;

    /// <inheritdoc />
    public bool Contains(IQueryParameter item) => _parameters.Contains(item);

    /// <inheritdoc />
    public bool IsProperSubsetOf(IEnumerable<IQueryParameter> other) => _parameters.IsProperSubsetOf(other);

    /// <inheritdoc />
    public bool IsProperSupersetOf(IEnumerable<IQueryParameter> other) => _parameters.IsProperSupersetOf(other);

    /// <inheritdoc />
    public bool IsSubsetOf(IEnumerable<IQueryParameter> other) => _parameters.IsSubsetOf(other);

    /// <inheritdoc />
    public bool IsSupersetOf(IEnumerable<IQueryParameter> other) => _parameters.IsSupersetOf(other);

    /// <inheritdoc />
    public bool Overlaps(IEnumerable<IQueryParameter> other) => _parameters.Overlaps(other);

    /// <inheritdoc />
    public bool SetEquals(IEnumerable<IQueryParameter> other) => _parameters.SetEquals(other);
}
