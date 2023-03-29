using System.Windows.Input;
using Aosta.Core.Data.Models;
using Aosta.Core.Data.Ordering;
using Aosta.Core.Extensions;
using Aosta.GUI.Features.AnimeManualAddPage;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Realms;

namespace Aosta.GUI.Features.MainPage;

[ObservableObject]
public partial class MainPageViewModel : RealmViewModel
{
    public IEnumerable<ContentObject> RealmAnimeList { get; set; }

    private readonly ILogger _logger;

    public MainPageViewModel(ILogger<MainPageViewModel> logger)
    {
        _logger = logger;
        RealmAnimeList = Realm.All<ContentObject>().OrderBy(AnimeOrdering.ByTitle);
    }

    public ICommand AddAnimeCommand => new Command(async () =>
    {
        await Shell.Current.GoToAsync(nameof(AddAnimePage));
    });

    public ICommand GoToCommand => new Command<Type>(async pageType =>
    {
        _logger.LogDebug("Navigated to: {0}", pageType.Name);
        await Shell.Current.GoToAsync($"{pageType.Name}");
    });
}
