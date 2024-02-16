// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class OnlineEpisodeDetailsViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly long _animeId;
    public AnimeEpisodeResponse Response { get; }

    public OnlineEpisodeDetailsViewModel(IScreen host, AnimeEpisodeResponse response, long animeId)
    {
        HostScreen = host;
        UrlPathSegment = $"jikan-ep-{animeId}-{response.MalId}";

        _animeId = animeId;
        Response = response;
        PageTitle = ("Label.Episode.Number", response.MalId).Localize();

        Observable.StartAsync(getData);
    }

    internal string Score => Response.Score?.ToString("0.00") ?? LocalizedString.NA;

    internal LocalizedString PageTitle { get; }

    internal string Title => Response.Title ?? $"{Response.MalId} - {LocalizedString.NOT_AVAILABLE}";

    [Reactive]
    internal LocalizedString? Duration { get; private set; }

    [Reactive]
    internal string? Synopsis { get; set; }

    private async Task getData(CancellationToken ct = default)
    {
        var jikan = Locator.Current.GetSafely<IJikan>();

        var result = await jikan.GetAnimeEpisodeAsync(_animeId, Response.MalId, ct);

        Synopsis = result.Data.Synopsis ?? LocalizedString.NOT_AVAILABLE;
        Duration = result.Data.Duration switch
        {
            null => LocalizedString.NOT_AVAILABLE,
            not null => LocalizedString.Duration(TimeSpan.FromSeconds(result.Data.Duration.Value))
        };
    }

    public void Dispose()
    {
        Duration?.Dispose();
        PageTitle.Dispose();
    }
}
