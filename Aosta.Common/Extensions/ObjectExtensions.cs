// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics.CodeAnalysis;

namespace Aosta.Common.Extensions;

public static class ObjectExtensions
{
    /// <summary>
    /// Coerces a nullable object as non-nullable. This is a functional alternative to the null-forgiving operator "<c>!</c>".
    /// </summary>
    /// <remarks>
    /// This should only be used when an assertion or other handling is not a reasonable alternative.
    /// </remarks>
    /// <param name="obj">The nullable object.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The non-nullable object corresponding to <paramref name="obj"/>.</returns>
    [return: NotNull]
    public static T AsNonNull<T>(this T? obj) => obj!;

    /// <summary>
    /// Coerces a nullable object as non-nullable. If the object is null returns the provided fallback value instead.
    /// </summary>
    /// <param name="obj">The nullable object.</param>
    /// <param name="fallback">The fallback value for the object.</param>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <returns>The non-nullable object corresponding to <paramref name="obj"/>, or <paramref name="fallback"/> if <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">When the provided fallback value is <c>null</c>.</exception>
    [return: NotNull]
    public static T OrDefault<T>(this T? obj, T fallback) => obj ?? fallback ??
        throw new ArgumentNullException(nameof(fallback), "The fallback value was null");
}
