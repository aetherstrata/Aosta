// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

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

using Realms;

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
        DetailsPill = InfoPill.Create(response);

        if (HasEpisodes)
        {
            Observable.Start(() => UpdateEpisodesList());
        }
    }

    public IContentInfoPill DetailsPill { get; }

    internal string? LargeBanner => _response.Images?.JPG?.LargeImageUrl;

    internal string Synopsis => _response.Synopsis ?? LocalizedString.NOT_AVAILABLE;

    internal string Title => _response.Titles.GetDefault() ?? LocalizedString.NA;

    internal bool HasEpisodes => _response.Type != AnimeType.Movie && _response.Episodes > 0;

    internal ObservableCollection<JikanEpisodeEntry> Episodes { get; } = [];

    [Reactive]
    internal bool IsLoadEpisodesButtonVisible { get; set; }

    internal bool CanBeAdded()
    {
        return !_realm.All<Anime>()
            .Is(static x => x.ID, _response.MalId)
            .Any();
    }

    internal Task AddToRealm(CancellationToken ct = default)
    {
        this.Log().Info("Writing anime {Title} [{Id}] to Realm",
            _response.Titles.GetDefault() ?? "N/A",
            _response.MalId);

        return Locator.Current.GetSafely<RealmAccess>().WriteAsync(r =>
        {
            var model = _response.ToModel();
            model.Episodes.AddRange(Episodes.Select(x => x.Response.ToModel()));
            r.Add(model);

        }, ct);
    }

    private int _page;
    internal Task UpdateEpisodesList(CancellationToken ct = default)
    {
        Interlocked.Increment(ref _page);

        this.Log().Debug<JikanAnimeDetailsViewModel>("Getting episodes page {Page} for anime {Name} [{Id}]",
            _page, _response.Titles.GetDefault()!, _response.MalId);

        return _jikan.GetAnimeEpisodesAsync(_response.MalId, _page, ct).ContinueWith(task =>
        {
            var entries = task.Result.Data.Select(x => new JikanEpisodeEntry(HostScreen, x, _response.MalId));

            Episodes.AddRange(entries);

            if (task.Result.Pagination.HasNextPage)
            {
                IsLoadEpisodesButtonVisible = true;
            }
        }, ct);
    }
}
