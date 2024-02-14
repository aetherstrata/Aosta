// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reactive;

using Aosta.Ava.Localization;
using Aosta.Data.Models;

using ReactiveUI;

namespace Aosta.Ava.ViewModels.Card;

public class AnimeListCardViewModel : ReactiveObject, IOnlineCard
{
    private readonly Anime _data;

    public AnimeListCardViewModel(IScreen host, Anime data)
    {
        _data = data;

        GoToDetails = ReactiveCommand.CreateFromObservable(() => host.Router.Navigate.Execute(new LocalAnimeDetailsViewModel(host, _data)));
    }

    public string Title => _data.GetDefaultTitle() ?? LocalizedString.NOT_AVAILABLE;

    public string BannerUrl => _data.Images?.JPG?.ImageUrl ?? IOnlineCard.PORTRAIT_PLACEHOLDER;

    public ReactiveCommand<Unit,IRoutableViewModel> GoToDetails { get; }
}
