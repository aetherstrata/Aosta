// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.ObjectModel;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Core.Data;
using Aosta.Core.Database;
using Aosta.Core.Database.Models;

using DynamicData;

using ReactiveUI;

using Realms;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava.ViewModels;

public sealed class AnimeListPageViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    private readonly ILogger _logger = Locator.Current.GetSafely<ILogger>();
    private readonly Realm _realm = Locator.Current.GetSafely<RealmAccess>().GetRealm();

    /// <inheritdoc />
    public string? UrlPathSegment => "list";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public AnimeListPageViewModel(IScreen host)
    {
        HostScreen = host;

        var list = _realm.All<Anime>().AsRealmCollection();

        AnimeCount = list.Count == 0
            ? Localizer.Instance["AnimeList.Header.NoAnime"]
            : $"{list.Count} anime";

        AnimeList.AddRange(list.Select(a => new AnimeListCardViewModel(HostScreen, a)));

        list.SubscribeForNotifications((sender, changes) =>
        {
            if (changes is null) return;

            for (int i = 0; i < changes.DeletedIndices.Length; i++)
            {
                AnimeList.RemoveAt(changes.DeletedIndices[i]);
            }

            for (int i = 0; i < changes.InsertedIndices.Length; i++)
            {
                AnimeList.Insert(changes.InsertedIndices[i], new AnimeListCardViewModel(HostScreen, sender[i]));
            }

            for (int i = 0; i < changes.NewModifiedIndices.Length; i++)
            {
                AnimeList.Insert(changes.NewModifiedIndices[i], new AnimeListCardViewModel(HostScreen, sender[i]));
            }

            AnimeCount = changes.IsCleared
                ? Localizer.Instance["AnimeList.Header.NoAnime"]
                : $"{sender.Count} anime";
        });
    }

    public ObservableCollection<AnimeListCardViewModel> AnimeList { get; } = new();

    private string _animeCount = Localizer.Instance["AnimeList.Header.NoAnime"];
    public string AnimeCount
    {
        get => _animeCount;
        set
        {
            _logger.Debug("Got {AnimeCount} in the user list", value);
            this.RaiseAndSetIfChanged(ref _animeCount, value);
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _realm.Dispose();
    }
}
