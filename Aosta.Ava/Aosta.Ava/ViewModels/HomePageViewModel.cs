using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Aosta.Ava.Extensions;
using Aosta.Core;
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
                var vm = new AnimeCardViewModel(hostScreen, anime);

                TopAnimes.Add(vm);
            }
        });
    }

    public ObservableCollection<AnimeCardViewModel> TopAnimes { get; } = new();
}
