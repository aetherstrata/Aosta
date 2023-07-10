namespace Aosta.Jikan.Query;

internal interface IQueryParameterList
{
    IEnumerable<IQueryParameter> GetParameters();
}