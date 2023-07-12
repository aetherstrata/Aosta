namespace Aosta.Jikan.Query;

/// <summary>
/// An <see cref="IQuery"/> encapsulates an HTTP request as an object
/// </summary>
internal interface IQuery
{
    /// <summary>
    /// Get the query string
    /// </summary>
    /// <returns>Returns the complete query string</returns>
    string GetQuery();
}