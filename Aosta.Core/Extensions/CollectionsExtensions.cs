// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Core.Database.Models.Embedded;

namespace Aosta.Core.Extensions;

public static class CollectionExtensions
{
    public static string GetDefault(this ICollection<TitleEntry> entries)
    {
        return entries.First(entry => entry.Type == "Default").Title;
    }
}
