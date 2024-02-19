// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.DetailsPill;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Data.Mapper;
using Aosta.Data.Models;
using Aosta.Jikan;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class OnlineAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    private readonly IJikan _jikan = Locator.Current.GetSafely<IJikan>();
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public AnimeResponse Response { get; }

    internal OnlineAnimeDetailsViewModel(IScreen hostScreen, AnimeResponse response)
    {
        UrlPathSegment = $"jikan-details-{response.MalId}";
        HostScreen = hostScreen;

        Response = response;
        Status = response.Status.Localize();
        DetailsPill = InfoPill.Create(response);

        if (HasEpisodes)
        {
            Observable.Start(() => UpdateEpisodesList());
        }
    }

    public IContentInfoPill DetailsPill { get; }

    internal string? LargeBanner => Response.Images?.JPG?.LargeImageUrl;

    internal string Title => Response.Titles.GetDefault() ?? LocalizedString.NA;

    internal bool HasEpisodes => Response.Type != AnimeType.Movie && Response.Episodes > 0;

    internal ObservableCollection<OnlineEpisodeEntry> Episodes { get; } = [];

    [Reactive]
    internal bool IsLoadEpisodesButtonVisible { get; set; }

    public string Score => Response.Score?.ToString("0.00") ?? LocalizedString.NA;

    public LocalizedString Status { get; }

    internal bool CanBeAdded()
    {
        return !_realm.Run(r => r
            .All<Anime>()
            .Is(static x => x.ID, Response.MalId)
            .Any());
    }

    internal Task AddToRealm(CancellationToken ct = default)
    {
        this.Log().Info("Writing anime {Title} [{Id}] to Realm",
            Response.Titles.GetDefault() ?? "N/A",
            Response.MalId);

        return Locator.Current.GetSafely<RealmAccess>().WriteAsync(r =>
        {
            var model = Response.ToModel();
            model.Episodes.AddRange(Episodes.Select(x => x.Response.ToModel()));
            r.Add(model);
        }, ct);
    }

    private int _page;

    internal Task UpdateEpisodesList()
    {
        Interlocked.Increment(ref _page);

        this.Log().Debug<OnlineAnimeDetailsViewModel>("Getting episodes page {Page} for anime {Name} [{Id}]",
            _page, Response.Titles.GetDefault()!, Response.MalId);

        return _jikan.GetAnimeEpisodesAsync(Response.MalId, _page).ContinueWith(task =>
        {
            var entries = task.Result.Data.Select(x => new OnlineEpisodeEntry(HostScreen, x, Response.MalId));

            Episodes.AddRange(entries);

            IsLoadEpisodesButtonVisible = task.Result.Pagination.HasNextPage;
        });
    }

    public void Dispose()
    {
        Status.Dispose();
        DetailsPill.Dispose();
    }
}
