using System.Collections;

namespace Aosta.Jikan.Query;

public class JikanQueryParameterSet : IEnumerable<IQueryParameter>
{
    private readonly HashSet<IQueryParameter> _parameters = new(QueryParameter.NameComparer);

    public static readonly JikanQueryParameterSet Empty = new JikanQueryParameterSet();

    internal JikanQueryParameterSet()
    {
    }

    internal void Add(IQueryParameter parameter)
    {
        bool result = _parameters.Add(parameter);
        if(!result) throw new JikanDuplicateParameterException($"A query parameter named {parameter.GetName()} already exists.", nameof(parameter));
    }

    internal void Add<T>(string name, T value)
    {
        Add(new JikanQueryParameter<T>()
        {
            Name = name,
            Value = value
        });
    }

    internal void Add(string name)
    {
        Add(new JikanQueryParameter<bool>()
        {
            Name = name,
            Value = true
        });
    }

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