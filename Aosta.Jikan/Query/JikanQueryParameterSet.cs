using System.Collections;
using Aosta.Common.Extensions;

namespace Aosta.Jikan.Query;

public class JikanQueryParameterSet : IEnumerable<IQueryParameter>
{
    private readonly HashSet<IQueryParameter> _parameters = new(QueryParameter.NameComparer);

    public static readonly JikanQueryParameterSet Empty = new();

    internal JikanQueryParameterSet()
    {
    }

    internal void Add(IQueryParameter parameter)
    {
        bool result = _parameters.Add(parameter);
        if(!result) throw new JikanDuplicateParameterException($"A query parameter named {parameter.GetName()} already exists.", nameof(parameter));
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
        Add(new BoolQueryParameter()
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

    public IEnumerator<IQueryParameter> GetEnumerator()
    {
        return _parameters.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}