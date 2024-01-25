namespace Aosta.Jikan.Exceptions;

[Serializable]
internal class JikanParameterValidationException : ArgumentException
{
    internal JikanParameterValidationException(string message, string argumentName) : base(message, argumentName)
    {
    }

    internal JikanParameterValidationException(string message) : base(message)
    {
    }
}
