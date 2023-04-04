namespace Aosta.Core.Consts;

public static class ErrorMessages
{
    public const string FailedRequest = "GET request failed. Status code: {0} Inner message: {1}";
    public const string SerializationFailed = "Serialization failed.";
    public const string SerializationNullResult = "Deserialized object was null.";

    public static string FailedEnumToStringConversion<T>(T parsedEnum) where T : Enum
    {
        return $"Tried to convert {typeof(T)} enum [{Convert.ToInt32(parsedEnum)}] but had an unexpected value.";
    }
}