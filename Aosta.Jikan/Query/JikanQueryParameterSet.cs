using System.Text.RegularExpressions;

using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Query;

public partial class JikanQueryParameterSet
{
    private readonly HashSet<IQueryParameter> _inner = new(QueryParameter.NameComparer);

    [GeneratedRegex(@"^\d{4}(-\d{2}){0,2}$", RegexOptions.CultureInvariant, 1)]
    protected static partial Regex DateRegex();

    internal JikanQueryParameterSet()
    {
    }

    internal void Add<T>(string name, T value) where T : struct, Enum
    {
        add(new EnumQueryParameter<T>
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, double value)
    {
        add(new JikanQueryParameter<double>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, long value)
    {
        add(new JikanQueryParameter<long>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, int value)
    {
        add(new JikanQueryParameter<int>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, string value)
    {
        add(new JikanQueryParameter<string>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name, char value) => Add(name, value.ToString());

    internal void Add(string name, bool value)
    {
        add(new BoolQueryParameter
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name) => Add(name, true);

    internal void AddRange(JikanQueryParameterSet other)
    {
        foreach (var val in other._inner)
        {
            add(val);
        }
    }

    private void add(IQueryParameter parameter)
    {
        if (_inner.Add(parameter)) return;

        throw new JikanDuplicateParameterException($"Parameter {parameter.GetName()} was set already");
    }

    public override string ToString()
    {
        string parameterString = string.Join("&", _inner
            .Select(x => x.ToString())
            .Where(s => !string.IsNullOrEmpty(s)));

        return string.IsNullOrEmpty(parameterString)
            ? string.Empty
            : "?" + parameterString;
    }
}
