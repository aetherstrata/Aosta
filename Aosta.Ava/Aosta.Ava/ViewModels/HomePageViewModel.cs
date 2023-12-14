using System.Collections.ObjectModel;
using System.Reactive.Linq;

using Aosta.Core;
using Aosta.Jikan;

using ReactiveUI;

namespace Aosta.Ava.ViewModels;

public class HomePageViewModel : ReactiveObject, IRoutableViewModel
{
    /// <inheritdoc />
    public string? UrlPathSegment => "home";

    /// <inheritdoc />
    public IScreen HostScreen { get; }

    public HomePageViewModel(IScreen hostScreen, IJikan jikan, AostaDotNet aosta)
    {
        HostScreen = hostScreen;

        Observable.Start(() =>
        {
            var top = jikan.GetTopAnimeAsync().Result.Data;

            foreach (var anime in top)
            {
                var vm = new AnimeCardViewModel(hostScreen, aosta, anime);

                TopAnimes.Add(vm);
            }
        });
    }

    public ObservableCollection<AnimeCardViewModel> TopAnimes { get; } = new();
}
