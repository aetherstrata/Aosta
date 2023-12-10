namespace Aosta.Jikan;

[Serializable]
internal class JikanDuplicateParameterException : ArgumentException
{
    internal JikanDuplicateParameterException(string message, string argumentName) : base(message, argumentName)
    {
    }

    internal JikanDuplicateParameterException(string message) : base(message)
    {
    }
}