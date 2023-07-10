namespace Aosta.Jikan.Query;

internal abstract class Query : IQuery
{
    private readonly string[] _endpoint;

    protected List<IQueryParameter> Parameters { get; } = new();

    protected Query(string[] endpoint)
    {
        _endpoint = endpoint;
    }

    public string GetQuery()
    {
        return string.Join("/", _endpoint) + (Parameters.Any() ? "?" + string.Join("&", Parameters.Select(x => x.ToString())) : string.Empty);
    }
}