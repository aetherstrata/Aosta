using System.ComponentModel;
using System.Reflection;

namespace Aosta.Core.Extensions;

internal static class EnumExtensions
{
	[Obsolete]
	public static string? GetDescription<T>(this T source) where T : Enum => typeof(T)
		.GetField(source.ToString())
		?.GetCustomAttribute<DescriptionAttribute>()
		?.Description;
}