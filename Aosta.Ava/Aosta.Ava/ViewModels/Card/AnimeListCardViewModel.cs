// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Ava.ViewModels.Interfaces;
using Aosta.Core.Database.Models;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class AnimeListCardViewModel(IScreen host, Anime data) : ReactiveObject, IOnlineCardViewModel
{
    public string Title => data.DefaultTitle;

    public string BannerUrl => data.Jikan?.Images?.JPG?.ImageUrl ?? IOnlineCardViewModel.PORTRAIT_PLACEHOLDER;
    public ReactiveCommand<Unit,IRoutableViewModel> GoToDetails { get; }
}
