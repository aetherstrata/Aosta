namespace Aosta.Jikan.Query;

// ReSharper disable once UnusedTypeParameter
/// <summary>
/// An encapsulation of an HTTP request
/// </summary>
/// <typeparam name="T">Return type of the query</typeparam>
internal interface IQuery<out T>
{
    /// <summary>
    /// Get the query string
    /// </summary>
    /// <returns>Returns the complete query string</returns>
    string GetQuery();
}
