// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.Extensions;

public static class AvaloniaExtensions
{
    internal static bool CanGoBack(this IScreen host)
    {
        return host.Router.NavigationStack.Count > 1;
    }

    public static T GetSafely<T>(this IReadonlyDependencyResolver resolver, string? contract = null)
    {
        return resolver.GetService<T>(contract) ?? throw new ArgumentNullException(typeof(T).FullName,
            "The resolver does not contain an instance of the provided type");
    }
}
