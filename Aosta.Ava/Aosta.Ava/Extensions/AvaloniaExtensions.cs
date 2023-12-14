// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using ReactiveUI;

namespace Aosta.Ava.Extensions;

internal static class AvaloniaExtensions
{
    internal static bool CanGoBack(this IScreen host)
    {
        return host.Router.NavigationStack.Count > 1;
    }
}
