using System.Text;

namespace Aosta.Common.Extensions;

public static class StringExtensions
{
    public static StringBuilder Prepend(this StringBuilder sb, string content)
    {
        return sb.Insert(0, content);
    }

    public static StringBuilder Prepend(this StringBuilder sb, char content)
    {
        return sb.Insert(0, content);
    }

    public static string ToStringLower(this bool value) => value switch
    {
        true => "true",
        false => "false"
    };
}
