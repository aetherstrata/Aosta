using Aosta.Core;
using Aosta.Core.Database.Models;
using Aosta.Core.Database.Ordering;
using Aosta.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Aosta.GUI.Features.MainPage;

[ObservableObject]
public partial class MainPageViewModel : RealmViewModel
{
    public IEnumerable<Anime> RealmAnimeList { get; set; }

    [ObservableProperty]
    private bool _labelsVisible;

    private readonly AostaDotNet _aosta;

    public MainPageViewModel(AostaDotNet aosta) : base(aosta)
    {
        _aosta = aosta;
        RealmAnimeList = Realm.All<Anime>().OrderBy(AnimeOrdering.ByTitle);
    }

    [RelayCommand]
    private async Task GoToPage(Type pageType)
    {
        _aosta.Log.Debug("Navigated to: {Name}", pageType.Name);
        await Shell.Current.GoToAsync($"{pageType.Name}");
    }

    [RelayCommand]
    private void AddButton()
    {
        LabelsVisible = !LabelsVisible;
    }
}
