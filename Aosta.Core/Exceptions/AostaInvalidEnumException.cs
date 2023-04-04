using Aosta.Core.Consts;

namespace Aosta.Core.Exceptions;

/// <summary>
/// Exception class thrown when trying to convert an invalid enums
/// </summary>
public class AostaInvalidEnumException<T> : ArgumentException where T : Enum
{
    public T Enum { get; }

    public AostaInvalidEnumException(T parameter) : base(ErrorMessages.FailedEnumToStringConversion(parameter))
    {
        Enum = parameter;
    }
}