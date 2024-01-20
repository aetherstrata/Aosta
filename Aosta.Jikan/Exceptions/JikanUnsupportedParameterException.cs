namespace Aosta.Jikan.Exceptions;

public class JikanUnsupportedParameterException : ArgumentException
{
    internal JikanUnsupportedParameterException(string message, string paramName) : base(message, paramName)
    {
    }
}