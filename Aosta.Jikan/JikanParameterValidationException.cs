namespace Aosta.Jikan;

/// <summary>
/// Exception class thrown when input parameters are invalid.
/// </summary>
public class JikanParameterValidationException : ArgumentException
{
	public JikanParameterValidationException(string message) : base(message)
	{
	}

	public JikanParameterValidationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public JikanParameterValidationException(string message, string argumentName) : base(message, argumentName)
	{
	}

	public JikanParameterValidationException(string message, string argumentName, Exception innerException) : base(message, argumentName, innerException)
	{
	}
}