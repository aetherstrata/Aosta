using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Settings;
using Aosta.Ava.ViewModels.Card;
using Aosta.Jikan;
using Aosta.Jikan.Query.Parameters;

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

        Observable.Start(async () =>
        {
            var jikan = Locator.Current.GetSafely<IJikan>();

            var topTask = jikan.GetTopAnimeAsync(TopAnimeQueryParameters.Create()
                .SafeForWork(!Setting.IncludeNsfw));

            var currentTask = jikan.GetCurrentSeasonAsync(SeasonQueryParameters.Create()
                .SafeForWork(!Setting.IncludeNsfw));

            var results = await Task.WhenAll(topTask, currentTask);

            foreach (var anime in results[0].Data)
            {
                var vm = new JikanAnimeCardViewModel(HostScreen, anime);

                TopAnimes.Add(vm);
            }

            foreach (var anime in results[1].Data)
            {
                var vm = new JikanAnimeCardViewModel(HostScreen, anime);

                CurrentAnimes.Add(vm);
            }
        });

        var settingsPage = new Lazy<SettingsViewModel>(() => new SettingsViewModel(HostScreen));

        GoToSettings = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(settingsPage.Value));
    }

    public ObservableCollection<JikanAnimeCardViewModel> TopAnimes { get; } = [];
    public ObservableCollection<JikanAnimeCardViewModel> CurrentAnimes { get; } = [];

    public ReactiveCommand<Unit, IRoutableViewModel> GoToSettings { get; }
}
