using System.Runtime.Serialization;

namespace Aosta.Jikan;

[Serializable]
public class JikanParameterValidationException : ArgumentException
{
    internal JikanParameterValidationException(string message, string argumentName) : base(message, argumentName)
    {
    }

    protected JikanParameterValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}