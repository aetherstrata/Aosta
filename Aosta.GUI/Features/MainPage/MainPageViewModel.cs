using Aosta.Core;
using Aosta.Core.Database.Models;
using Aosta.Core.Database.Ordering;
using Aosta.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace Aosta.GUI.Features.MainPage;

public partial class MainPageViewModel : ObservableObject
{
    public IEnumerable<Anime> RealmAnimeList { get; set; }

    [ObservableProperty]
    private bool _labelsVisible;

    public MainPageViewModel(AostaDotNet aosta)
    {
        var realm = aosta.GetInstance();
        RealmAnimeList = realm.All<Anime>().OrderBy(AnimeOrdering.ByTitle);
    }

    [RelayCommand]
    private async Task GoToPage(Type pageType)
    {
        Log.Debug("Navigated to: {Name}", pageType.Name);
        await Shell.Current.GoToAsync($"{pageType.Name}", true);
    }

    [RelayCommand]
    private void AddButton()
    {
        LabelsVisible = !LabelsVisible;
    }
}
