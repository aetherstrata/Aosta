using System.Windows.Input;
using Aosta.Core.Data.Realm;
using Aosta.GUI.Features.AnimeManualAddPage;
using CommunityToolkit.Mvvm.ComponentModel;
using Realms;

namespace Aosta.GUI.Features.MainPage;

[ObservableObject]
public partial class MainPageViewModel
{
    private Realm _realm;

    public IEnumerable<AnimeObject> RealmAnimeList { get; set; }

    public MainPageViewModel()
    {
        _realm = App.Core.GetInstance();
        RealmAnimeList = _realm.All<AnimeObject>().OrderBy(anime => anime.Title);
    }

    public ICommand AddAnimeCommand => new Command(async () =>
    {
        await AppShell.Current.GoToAsync(nameof(AddAnimePage));
    });

    public ICommand GoToCommand => new Command<Type>(
        async (Type pageType) => { await AppShell.Current.GoToAsync($"{pageType.Name}"); });
}