using System.Windows.Input;
using Aosta.Core.Data;
using Aosta.Core.Data.Realm;
using Aosta.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using JikanDotNet;
using Realms;

namespace Aosta.GUI.Features.SettingsPage;

[ObservableObject]
public partial class SettingsViewModel
{
    IJikan jikan;
    ISettingsService settingsService;

    private Realm realm;

    [ObservableProperty] private string _path = Globals.Location.AppData;

    [ObservableProperty] private string _objectCount = "N/A";

    [ObservableProperty] private string _anime = "AHA";

    [ObservableProperty] private bool _darkModeSwitch = Application.Current?.UserAppTheme == AppTheme.Dark;

    public SettingsViewModel(ISettingsService settingsService, IJikan jikan)
    {
        this.jikan = jikan;
        this.settingsService = settingsService;
        this.realm = App.Core.GetInstance();

        ObjectCount = realm.All<AnimeObject>().Count().ToString();
    }

    public async Task LoadAssetToString(string fileName)
    {
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
        using StreamReader reader = new StreamReader(fileStream);

        Path = await reader.ReadToEndAsync();
    }

    public ICommand GotoOnboardingCommand => new Command(async () =>
    {
        await AppShell.Current.GoToAsync($"//{nameof(OnboardingPage.OnboardingPage)}");
    });

    public ICommand PrintAnimeCount => new Command(async () =>
    {
        //File.Delete(Globals.Location.Database);


        await realm.WriteAsync(() =>
        {
            var anime = new AnimeObject
            {
                Type = ContentType.TV,
                Title = "Paolo"
            };
            realm.Add(anime);
        });

        ObjectCount = realm.All<AnimeObject>().Count().ToString();
    });

    public ICommand DeleteRealmFile => new Command(() =>
    {
        realm.Dispose();
        File.Delete(Globals.Location.Database);
        realm = App.Core.GetInstance();
        ObjectCount = realm.All<AnimeObject>().Count().ToString();
    });

    public ICommand UpdateTheme => new Command(async () =>
    {
        if (DarkModeSwitch)
        {
            Application.Current!.UserAppTheme = AppTheme.Dark;
            await settingsService.Save<bool>("useDarkTheme", true);
        }
        else
        {
            Application.Current!.UserAppTheme = AppTheme.Light;
            await settingsService.Save<bool>("useDarkTheme", false);
        }
    });
}