using System.Diagnostics;
using System.Windows.Input;
using Aosta.Core;
using Aosta.Core.Database.Models;
using Aosta.Core.Database.Models.Jikan;
using Aosta.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Aosta.GUI.Features.SettingsPage;

[ObservableObject]
public partial class SettingsViewModel : RealmViewModel
{
    [ObservableProperty]
    private string _anime = "AHA";

    [ObservableProperty]
    private bool _darkModeSwitch = Application.Current?.UserAppTheme == AppTheme.Dark;

    [ObservableProperty]
    private string _objectCount = "N/A";

    [ObservableProperty]
    private string _path;

    private readonly AostaDotNet _aosta;

    private int count = 1;

    private readonly ISettingsService _settingsService;

    public SettingsViewModel(ISettingsService settingsService, AostaDotNet aosta) : base(aosta)
    {
        _aosta = aosta;
        _settingsService = settingsService;
        _path = FileSystem.Current.AppDataDirectory;

        UpdateRealmCount();
    }

    [RelayCommand]
    public async Task GotoOnboarding()
    {
        await Shell.Current.GoToAsync($"{nameof(OnboardingPage)}");
    }

    [RelayCommand]
    public async Task PrintAnimeCount()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("No internet! Jikan will fail...");
        }

        await _aosta.CreateJikanContentAsync(count);

        await Realm.WriteAsync(() =>
        {
            Realm.Add(new Anime()
            {
                Title = "Pippo " + count,
                Jikan = Realm.Find<JikanAnime>(count)
            });
        });

        count++;
        ObjectCount = Realm.All<Anime>().Count().ToString();
    }

    public ICommand DeleteAllData => new Command(async () =>
    {
        await Realm.WriteAsync(() =>
        {
            Realm.RemoveAll();
        });
        UpdateRealmCount();
    });

    public ICommand UpdateTheme => new Command(async () =>
    {
        if (DarkModeSwitch)
        {
            Application.Current!.UserAppTheme = AppTheme.Dark;
            await _settingsService.Save("useDarkTheme", true);
        }
        else
        {
            Application.Current!.UserAppTheme = AppTheme.Light;
            await _settingsService.Save("useDarkTheme", false);
        }
    });

    public void UpdateRealmCount() => ObjectCount = Realm.All<Anime>().Count().ToString();
}