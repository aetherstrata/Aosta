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

    private readonly IJikan _jikan;
    private readonly AostaDotNet _aosta;

    public HomePageViewModel(IScreen hostScreen, IJikan jikan, AostaDotNet aosta)
    {
        HostScreen = hostScreen;
        _jikan = jikan;
        _aosta = aosta;

        Observable.Start(() =>
        {
            var top = _jikan.GetTopAnimeAsync().Result.Data;

            foreach (var anime in top)
            {
                var vm = new AnimeCardViewModel(anime);

                TopAnimes.Add(vm);
            }
        });
    }

    public ObservableCollection<AnimeCardViewModel> TopAnimes { get; } = new();
}
