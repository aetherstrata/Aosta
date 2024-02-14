// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Models.Embedded;

namespace Aosta.Data.Extensions;

public static class CollectionExtensions
{
    public static TitleEntry GetDefault(this ICollection<TitleEntry> entries)
    {
        return entries.First(entry => entry.Type == TitleEntry.DEFAULT_KEY);
    }

    public static TitleEntry? GetJapanese(this ICollection<TitleEntry> entries)
    {
        return entries.FirstOrDefault(entry => entry.Type == TitleEntry.JAPANESE_KEY);
    }

    public static TitleEntry? GetRomanji(this ICollection<TitleEntry> entries)
    {
        return entries.FirstOrDefault(entry => entry.Type == TitleEntry.ROMANJI_KEY);
    }
}
