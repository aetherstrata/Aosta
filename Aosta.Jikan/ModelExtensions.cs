// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Jikan.Models.Response;

namespace Aosta.Jikan;

public static class ModelExtensions
{
    public static TitleEntryResponse GetDefault(this ICollection<TitleEntryResponse> entries)
    {
        return entries.First(res => res.Type.Equals("Default"));
    }
}
