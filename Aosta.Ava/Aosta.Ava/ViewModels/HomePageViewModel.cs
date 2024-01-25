using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;
using Aosta.Ava.ViewModels.Card;
using Aosta.Ava.ViewModels.Settings;
using Aosta.Jikan;

using ReactiveUI;

using Splat;

namespace Aosta.Ava.ViewModels;

public class HomePageViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment => "home";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public HomePageViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;

        Observable.Start(() =>
        {
            var top = Locator.Current.GetSafely<IJikan>().GetTopAnimeAsync().Result.Data;

            foreach (var anime in top)
            {
                var vm = new TopAnimeCardViewModel(HostScreen, anime);

                TopAnimes.Add(vm);
            }
        });

        var settingsPage = new Lazy<SettingsViewModel>(() => new SettingsViewModel(HostScreen));

        GoToSettings = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(settingsPage.Value));
    }

    public ObservableCollection<TopAnimeCardViewModel> TopAnimes { get; } = new();

    public ReactiveCommand<Unit, IRoutableViewModel> GoToSettings { get; }
}
