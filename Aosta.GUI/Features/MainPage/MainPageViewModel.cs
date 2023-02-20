using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using Realms;
using Aosta.Core.Data.Realm;

namespace Aosta.GUI.ViewModels;

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
        await AppShell.Current.GoToAsync(nameof(Views.AddAnimePage));
    });

    public ICommand GoToCommand => new Command<Type>(
        async (Type pageType) => { await AppShell.Current.GoToAsync($"{pageType.Name}"); });
}