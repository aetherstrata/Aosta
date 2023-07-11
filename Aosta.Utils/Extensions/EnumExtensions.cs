using FastEnumUtility;

namespace Aosta.Utils.Extensions;

public static class EnumExtensions
{
    public static string StringValue<T>(this T e) where T : struct, Enum
    {
        return e.GetEnumMemberValue(false) ?? e.FastToString();
    }
}