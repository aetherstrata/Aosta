// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Ava.Localization;
using Aosta.Data.Models;
using Aosta.Jikan.Models.Response;

namespace Aosta.Ava.ViewModels.DetailsPill;

public class MoviePill : InfoPill
{
    public LocalizedString? Date { get; set; }

    public MoviePill(AnimeResponse response) : base(response)
    {
        if (response.Aired?.From.HasValue ?? false)
        {
            Date = LocalizedString.CompactDate(response.Aired.From.Value);
        }
    }

    public MoviePill(Anime model) : base(model)
    {
        if (model.Aired?.From.HasValue ?? false)
        {
            Date = LocalizedString.CompactDate(model.Aired.From.Value);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Date?.Dispose();
        }

        base.Dispose(disposing);
    }
}
