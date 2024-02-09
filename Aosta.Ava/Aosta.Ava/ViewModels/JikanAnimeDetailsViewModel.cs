// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Data;
using Aosta.Data.Database.Mapper;
using Aosta.Data.Database.Models;
using Aosta.Data.Extensions;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;

using Avalonia.ReactiveUI;

using DynamicData;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Realms;

using Serilog;

using Splat;

namespace Aosta.Ava.ViewModels;

public class JikanAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    private readonly IJikan _jikan = Locator.Current.GetSafely<IJikan>();
    private readonly Realm _realm;
    private readonly AnimeResponse _response;

    internal JikanAnimeDetailsViewModel(IScreen hostScreen, AnimeResponse response)
    {
        _realm = Locator.Current.GetSafely<RealmAccess>().GetRealm();
        _response = response;

        UrlPathSegment = $"jikan-details-{response.MalId}";
        HostScreen = hostScreen;

        _ = UpdateEpisodesList();
    }

    internal string? Banner => _response.Images?.JPG?.ImageUrl;

    internal string? LargeBanner => _response.Images?.JPG?.LargeImageUrl;

    internal string Score => _response.Score?.ToString("0.00") ?? LocalizedString.NA;

    internal LocalizedString Season => _response.Season?.Localize() ?? LocalizedString.NOT_AVAILABLE;

    internal string Synopsis => _response.Synopsis ?? LocalizedString.NOT_AVAILABLE;

    internal string Title => _response.Titles.GetDefault() ?? LocalizedString.NA;

    internal LocalizedString Type => _response.Type?.Localize() ?? LocalizedString.NA;

    internal string? Year => _response.Year.ToString();

    internal ObservableCollection<AnimeEpisodeResponse> Episodes { get; } = [];

    [Reactive]
    internal bool IsLoadEpisodesButtonVisible { get; set; }

    internal bool CanBeAdded()
    {
        return !_realm.All<Anime>()
            .Is(static x => x.Jikan!.ID, _response.MalId)
            .Any();
    }

    internal Task AddToRealm(CancellationToken ct = default)
    {
        this.Log().Info("Writing anime {Title} [{Id}] to Realm", _response.Titles.GetDefault() ?? "N/A", _response.MalId);
        return Locator.Current.GetSafely<RealmAccess>().WriteAsync(r => r.Add(_response.ToModel().NewRecord()), ct);
    }

    private int _page;
    internal async Task UpdateEpisodesList(CancellationToken ct = default)
    {
        _page++;

        this.Log().Debug<JikanAnimeDetailsViewModel>("Getting episodes page {Page} for anime {Id}", _page,
            _response.MalId);

        var result = await _jikan.GetAnimeEpisodesAsync(_response.MalId, _page, ct);

        Episodes.AddRange(result.Data);

        if (result.Pagination.HasNextPage) IsLoadEpisodesButtonVisible = true;
    }
}
