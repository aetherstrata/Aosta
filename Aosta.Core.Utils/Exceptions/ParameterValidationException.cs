namespace Aosta.Core.Utils.Exceptions;

/// <summary>
/// Exception class thrown when input parameters are invalid.
/// </summary>
public class ParameterValidationException : ArgumentException
{
	public ParameterValidationException(string message) : base(message)
	{
	}

	public ParameterValidationException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public ParameterValidationException(string message, string argumentName) : base(message, argumentName)
	{
	}

	public ParameterValidationException(string message, string argumentName, Exception innerException) : base(message, argumentName, innerException)
	{
	}
}