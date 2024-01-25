namespace Aosta.Jikan.Query;

// ReSharper disable once UnusedTypeParameter
/// <summary>
/// An encapsulation of an HTTP request
/// </summary>
internal interface IQuery
{
    /// <summary>
    /// Get the query string
    /// </summary>
    /// <returns>Returns the complete query string</returns>
    string GetQuery();
}
