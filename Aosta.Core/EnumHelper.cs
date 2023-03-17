using System.Runtime.CompilerServices;

namespace Aosta.Core;

public static class EnumHelper
{
    internal static T Parse<T>(string s) where T : struct, Enum
    {
        s = s.Replace(" ", "");

        if (Enum.GetNames<T>().Contains(s)) return Enum.Parse<T>(s);

        int na = -1;
        return Unsafe.As<int, T>(ref na);
    }
}