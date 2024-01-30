// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Database.Models.Embedded;

namespace Aosta.Data.Extensions;

public static class CollectionExtensions
{
    public static string? GetDefault(this ICollection<TitleEntry> entries)
    {
        return entries.FirstOrDefault(entry => entry.Type == "Default")?.Title;
    }
}
