using Aosta.Core.Data.Models;
using Aosta.Core.Data.Ordering;
using Aosta.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Aosta.GUI.Features.MainPage;

[ObservableObject]
public partial class MainPageViewModel : RealmViewModel
{
    public IEnumerable<AnimeObject> RealmAnimeList { get; set; }

    public MainPageViewModel()
    {
        RealmAnimeList = Realm.All<AnimeObject>().OrderBy(AnimeOrdering.ByTitle);
    }

    [RelayCommand]
    private async Task GoToPage(Type pageType)
    {
        App.Core.Log.Debug("Navigated to: {Name}", pageType.Name);
        await Shell.Current.GoToAsync($"{pageType.Name}");
    }
}
