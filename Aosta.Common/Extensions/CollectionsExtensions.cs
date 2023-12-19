namespace Aosta.Common.Extensions;

public static class CollectionsExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T item in source)
        {
            action(item);
        }
    }
}
