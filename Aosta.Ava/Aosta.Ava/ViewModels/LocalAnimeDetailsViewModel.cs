// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels.DetailsPill;
using Aosta.Data;
using Aosta.Data.Extensions;
using Aosta.Data.Models;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public sealed class LocalAnimeDetailsViewModel : ReactiveObject, IRoutableViewModel, IDisposable
{
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    /// <inheritdoc />
    public string? UrlPathSegment { get; }

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public Anime Anime { get; }

    internal LocalAnimeDetailsViewModel(IScreen host, Anime anime)
    {
        Anime = anime;

        HostScreen = host;
        UrlPathSegment = $"anime-details-{anime.ID}";

        Status = Anime.WatchingStatus.Localize();
        DetailsPill = InfoPill.Create(anime);
    }

    public IContentInfoPill DetailsPill { get; }

    public string DefaultTitle
    {
        get => Anime.Titles.GetDefault().Title;
        set
        {
            _realm.WriteAsync(r => Anime.Titles.GetDefault().Title = value);
            this.RaisePropertyChanged();
        }
    }

    public string Score => Anime.UserScore?.ToString() ?? LocalizedString.NA;

    public LocalizedString Status { get; }

    public void Dispose()
    {
        DetailsPill.Dispose();
        Status.Dispose();
    }
}
