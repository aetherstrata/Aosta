using Aosta.Utils.Consts;

namespace Aosta.Utils.Exceptions;

/// <summary>
/// Exception class thrown when trying to do operations on an invalid enums
/// </summary>
public class InvalidEnumException<T> : ArgumentException where T : Enum
{
    public T Enum { get; }

    public InvalidEnumException(T parameter, string message) : base(message)
    {
        Enum = parameter;
    }

    public InvalidEnumException(T parameter, string message, Exception innerException) : base(message, innerException)
    {
        Enum = parameter;
    }

    public InvalidEnumException(T parameter, string message, string paramName) : base(message, paramName)
    {
        Enum = parameter;
    }

    public InvalidEnumException(T parameter, string message, string paramName, Exception innerException) : base(message, paramName, innerException)
    {
        Enum = parameter;
    }

    public static InvalidEnumException<T> EnumMember(T e) => new(e, ErrorMessages.FailedEnumMemberLookup(e));

    public static InvalidEnumException<T> EnumToString(T e) => new(e, ErrorMessages.FailedEnumToStringConversion(e));
}