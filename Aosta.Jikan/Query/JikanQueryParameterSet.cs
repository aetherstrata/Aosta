using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Query;

public class JikanQueryParameterSet
{
    private readonly Dictionary<string, IQueryParameter> _inner = new();

    internal JikanQueryParameterSet()
    {
    }

    internal void Add<T>(string name, T value) where T : struct, Enum
    {
        _inner[name] = new EnumQueryParameter<T>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, double value)
    {
        _inner[name] = new JikanQueryParameter<double>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, long value)
    {
        _inner[name] = new JikanQueryParameter<long>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, int value)
    {
        _inner[name] = new JikanQueryParameter<int>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, string value)
    {
        _inner[name] = new JikanQueryParameter<string>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, char value) => Add(name, value.ToString());

    internal void Add(string name, bool value)
    {
        _inner[name] = new BoolQueryParameter
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name) => Add(name, true);

    internal void AddRange(JikanQueryParameterSet other)
    {
        foreach (var val in other._inner)
        {
            if (!_inner.TryAdd(val.Key, val.Value))
            {
                throw new JikanDuplicateParameterException($"Parameter {val.Key} was set already");
            }
        }
    }

    public override string ToString()
    {
        string parameterString = string.Join("&", _inner.Values
            .Select(x => x.ToString())
            .Where(s => !string.IsNullOrEmpty(s)));

        return string.IsNullOrEmpty(parameterString)
            ? string.Empty
            : "?" + parameterString;
    }
}
