// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data.Enums;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Anime = Aosta.Data.Models.Anime;

namespace Aosta.Ava.ViewModels.DetailsPill;

public class InfoPill : ReactiveObject, IContentInfoPill
{
    public static IContentInfoPill Create(AnimeResponse response)
    {
        return response.Type switch
        {
            AnimeType.TV => new AnimePill(response),
            AnimeType.Movie => new InfoPill(response),
            null => throw new ArgumentNullException(nameof(response),
                "Anime response did not have a valid content type"),
            _ => new EpisodesPill(response),
        };
    }

    public static IContentInfoPill Create(Anime model)
    {
        return model.Type switch
        {
            ContentType.TV => new AnimePill(model),
            ContentType.Movie => new InfoPill(model),
            null => throw new ArgumentNullException(nameof(model),
                "Anime response did not have a valid content type"),
            _ => new EpisodesPill(model),
        };
    }

    [Reactive]
    public string Score { get; set; }

    [Reactive]
    public string Year { get; set; }

    public LocalizedString Type { get; set; }

    public InfoPill(AnimeResponse response)
    {
        Score = response.Score?.ToString("0.00") ?? LocalizedString.NA;
        Year = response.Year?.ToString() ?? LocalizedString.NA;
        Type = response.Type.Localize();
    }

    public InfoPill(Anime model)
    {
        Score = model.UserScore?.ToString() ?? (model.Jikan?.Score != null
            ? $"{model.Jikan?.Score?.ToString("0.00")} ({Localizer.Instance["Label.Online"]})"
            : LocalizedString.NA);
        Year = model.Year?.ToString() ?? LocalizedString.NA;
        Type = model.Type.Localize();
    }
}

