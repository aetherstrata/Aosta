using System.Collections;

using Aosta.Jikan.Exceptions;

namespace Aosta.Jikan.Query;

public class JikanQueryParameterSet : Dictionary<string, IQueryParameter>
{
    internal JikanQueryParameterSet()
    {
    }

    internal void Add<T>(string name, T value) where T : struct, Enum
    {
        this[name] = new EnumQueryParameter<T>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, double value)
    {
        this[name] = new JikanQueryParameter<double>()
        {
            Name = name,
            Value = value
        };
    }


    internal void Add(string name, long value)
    {
        this[name] = new JikanQueryParameter<long>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, int value)
    {
        this[name] = new JikanQueryParameter<int>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, string value)
    {
        this[name] = new JikanQueryParameter<string>()
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name, char value) => Add(name, value.ToString());

    internal void Add(string name, bool value)
    {
        this[name] = new BoolQueryParameter
        {
            Name = name,
            Value = value
        };
    }

    internal void Add(string name) => Add(name, true);

    public override string ToString()
    {
        string parameterString = string.Join("&", Values
            .Select(x => x.ToString())
            .Where(s => !string.IsNullOrEmpty(s)));

        return string.IsNullOrEmpty(parameterString)
            ? string.Empty
            : "?" + parameterString;
    }
}
