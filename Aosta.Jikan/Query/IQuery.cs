namespace Aosta.Jikan.Query;

/// <summary>
/// An encapsulation of an HTTP request and its return type
/// </summary>
internal interface IQuery<T>
{
    /// <summary>
    /// Get the query string
    /// </summary>
    /// <returns>Returns the complete query string</returns>
    string GetQuery();
}