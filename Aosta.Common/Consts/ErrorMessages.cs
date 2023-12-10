using Aosta.Common.Extensions;

namespace Aosta.Common.Consts;

public static class ErrorMessages
{
    public const string FAILED_REQUEST = "GET request failed. Status code: {0} Inner message: {1}";
    public const string SERIALIZATION_FAILED = "Serialization failed.";
    public const string SERIALIZATION_NULL_RESULT = "Deserialized object was null.";

    public static string FailedEnumToStringConversion<T>(T parsedEnum) where T : struct, Enum
    {
        return $"Tried to convert enum {typeof(T)} [{parsedEnum.AsInt()}] but had an unexpected value.";
    }

    public static string FailedEnumMemberLookup<T>(T parsedEnum) where T : struct, Enum
    {
        return $"Tried to read EnumMember attribute for enum {typeof(T)} : {parsedEnum} but was not found.";
    }
}
