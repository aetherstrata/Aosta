namespace Aosta.Core.Exceptions;

/// <summary>
/// Exception class thrown when input parameters are invalid.
/// </summary>
public class AostaValidationException : Exception
{
	/// <summary>
	/// Name of the argument that failed validation.
	/// </summary>
	public string? ArgumentName { get; init; }

	/// <summary>
	/// Constructor with exception message and name of the argument that  failed validation.
	/// </summary>
	public AostaValidationException(string message, string argumentName) : base(message)
	{
		ArgumentName = argumentName;
	}
}