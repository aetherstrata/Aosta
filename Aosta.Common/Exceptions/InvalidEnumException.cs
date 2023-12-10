using Aosta.Common.Consts;

namespace Aosta.Common.Exceptions;

/// <summary>
/// Exception class thrown when trying to do operations on an invalid enums
/// </summary>
public class InvalidEnumException<T> : ArgumentException where T : struct, Enum
{
    public T Enum { get; }

    internal InvalidEnumException(T parameter, string message, string paramName) : base(message, paramName)
    {
        Enum = parameter;
    }

    public static InvalidEnumException<T> EnumMember(T e, string argumentName) => new(e, ErrorMessages.FailedEnumMemberLookup(e), argumentName);

    public static InvalidEnumException<T> EnumToString(T e, string argumentName) => new(e, ErrorMessages.FailedEnumToStringConversion(e), argumentName);
}
