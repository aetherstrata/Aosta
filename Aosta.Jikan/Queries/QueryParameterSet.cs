using System.Collections;

namespace Aosta.Jikan.Queries;

public class QueryParameterSet : IEnumerable<IQueryParameter>
{
    private readonly HashSet<IQueryParameter> _parameters = new(QueryParameter.NameComparer);

    public static readonly QueryParameterSet Empty = new QueryParameterSet();

    internal QueryParameterSet()
    {
    }

    internal void Add(IQueryParameter parameter)
    {
        bool result = _parameters.Add(parameter);
        if(!result) throw new JikanDuplicateParameterException($"A query parameter named {parameter.GetName()} already exists.", nameof(parameter));
    }

    internal void Add<T>(string name, T value)
    {
        Add(new QueryParameter<T>()
        {
            Name = name,
            Value = value
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