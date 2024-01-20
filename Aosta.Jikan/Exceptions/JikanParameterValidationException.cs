namespace Aosta.Jikan.Exceptions;

[Serializable]
public class JikanParameterValidationException : ArgumentException
{
    internal JikanParameterValidationException(string message, string argumentName) : base(message, argumentName)
    {
    }

    internal JikanParameterValidationException(string message) : base(message)
    {
    }
}