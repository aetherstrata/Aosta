// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Ava.Extensions;
using Aosta.Ava.ViewModels.Interfaces;
using Aosta.Core.Database;
using Aosta.Core.Database.Models;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class AnimeListCardViewModel : ReactiveObject, IOnlineCardViewModel
{
    private readonly Anime _data;
    private readonly RealmAccess _realm = Locator.Current.GetSafely<RealmAccess>();

    public AnimeListCardViewModel(IScreen host, Anime data)
    {
        _data = data;

        GoToDetails = ReactiveCommand.Create(() => _realm.Write(r => r.Remove(_data)));
    }

    public string Title => _data.DefaultTitle;

    public string BannerUrl => _data.Jikan?.Images?.JPG?.ImageUrl ?? IOnlineCardViewModel.PORTRAIT_PLACEHOLDER;

    public ReactiveCommand<Unit,Unit> GoToDetails { get; }
}
