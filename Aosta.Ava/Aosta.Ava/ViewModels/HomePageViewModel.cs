using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Aosta.Ava.Extensions;
using Aosta.Ava.Settings;
using Aosta.Jikan;
using Aosta.Jikan.Models.Response;
using Aosta.Jikan.Query.Parameters;

using DynamicData;

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

        Observable.StartAsync(async () =>
        {
            var jikan = Locator.Current.GetSafely<IJikan>();

            var topTask = jikan.GetTopAnimeAsync(TopAnimeQueryParameters.Create()
                .SafeForWork(!Setting.IncludeNsfw));

            var currentTask = jikan.GetCurrentSeasonAsync(SeasonQueryParameters.Create()
                .SafeForWork(!Setting.IncludeNsfw)
                .Unapproved(Setting.IncludeUnapproved));

            var results = await Task.WhenAll(topTask, currentTask);

            TopAnimes.AddRange(results[0].Data);

            CurrentAnimes.Add(results[1].Data);
        });

        var settingsPage = new Lazy<SettingsViewModel>(() => new SettingsViewModel(HostScreen));

        GoToSettings = ReactiveCommand.CreateFromObservable(() =>
            HostScreen.Router.Navigate.Execute(settingsPage.Value));

        GoToDetails = ReactiveCommand.CreateFromObservable((AnimeResponse response) =>
            HostScreen.Router.Navigate.Execute(new OnlineAnimeDetailsViewModel(HostScreen, response)));
    }

    public ObservableCollection<AnimeResponse> TopAnimes { get; } = [];
    public ObservableCollection<AnimeResponse> CurrentAnimes { get; } = [];

    public ReactiveCommand<Unit, IRoutableViewModel> GoToSettings { get; }
    public ReactiveCommand<AnimeResponse, IRoutableViewModel> GoToDetails { get; }
}
