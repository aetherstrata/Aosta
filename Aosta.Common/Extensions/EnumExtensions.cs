using System.Runtime.CompilerServices;
using FastEnumUtility;

namespace Aosta.Common.Extensions;

public static class EnumExtensions
{
    public static string StringValue<T>(this T e) where T : struct, Enum
    {
        return e.GetEnumMemberValue(false) ?? e.FastToString();
    }

    public static string FlagToString<T>(this T flagEnum) where T : struct, Enum
    {
        return string.Join(",", FastEnum.GetValues<T>()
            .Where(flag => flagEnum.HasFlag(flag))
            .Select(flag => flag.StringValue())
            .Where(str => !string.IsNullOrEmpty(str)));
    }

    public static bool HasSingleFlag<T>(this T flagEnum, T flag) where T : struct, Enum
    {
        return flagEnum.HasSingleFlag() && flagEnum.HasFlag(flag);
    }

    public static bool HasSingleFlag<T>(this T flagEnum) where T : struct, Enum
    {
        return (flagEnum.AsInt() & (flagEnum.AsInt() - 1)) == 0;
    }

    public static int AsInt<T>(this T e) where T : struct, Enum
    {
        return Unsafe.As<T, int>(ref e);
    }
}