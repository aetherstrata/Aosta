using Aosta.Core.Jikan.Models;

namespace Aosta.Core.Jikan;

/// <summary>
/// Exception class thrown when request is not handled properly.
/// </summary>
public class JikanRequestException : Exception
{
	/// <summary>
	/// Details of error returned from Jikan Api.
	/// </summary>
	public JikanApiError? ApiError { get; private set; }

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public JikanRequestException()
	{
	}

	/// <summary>
	/// Constructor with exception message and code.
	/// </summary>
	public JikanRequestException(string message, JikanApiError? apiError = null) : base(message)
	{
		ApiError = apiError;
	}

	/// <summary>
	/// Constructor with exception message and inner exception.
	/// </summary>
	public JikanRequestException(string message, Exception innerException) : base(message, innerException)
	{
	}
}