using System.Runtime.Serialization;

namespace Aosta.Jikan;

[Serializable]
internal class JikanDuplicateParameterException : ArgumentException
{
    internal JikanDuplicateParameterException(string message, string argumentName) : base(message, argumentName)
    {
    }

    protected JikanDuplicateParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}