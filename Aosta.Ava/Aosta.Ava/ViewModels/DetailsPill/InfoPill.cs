// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data.Enums;
using Aosta.Data.Models;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;

using ReactiveUI;

namespace Aosta.Ava.ViewModels.DetailsPill;

public abstract class InfoPill : ReactiveObject, IContentInfoPill
{
    public static InfoPill Create(AnimeResponse response)
    {
        return response.Type switch
        {
            AnimeType.TV => new AnimePill(response),
            AnimeType.Movie => new MoviePill(response),
            null => throw new ArgumentNullException(nameof(response),
                "Anime response did not have a valid content type"),
            _ => new EpisodesPill(response),
        };
    }

    public static InfoPill Create(Anime model)
    {
        return model.Type switch
        {
            ContentType.TV => new AnimePill(model),
            ContentType.Movie => new MoviePill(model),
            _ => new EpisodesPill(model),
        };
    }

    public LocalizedString Type { get; set; }

    protected InfoPill(AnimeResponse response)
    {
        Type = response.Type.Localize();
    }

    protected InfoPill(Anime model)
    {
        Type = model.Type.Localize();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Type.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

