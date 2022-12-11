using Aosta.GUI.Services;
using Aosta.GUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using JikanDotNet;
using Realms;
using System.Windows.Input;
using Aosta.Core;
using Aosta.Core.Data;
using Aosta.Core.Extensions;
using Aosta.Core.Realm;

namespace Aosta.GUI.ViewModels;

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

        ObjectCount = realm.All<ContentDTO>().Count().ToString();
    }

    public async Task LoadAssetToString(string fileName)
    {
        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
        using StreamReader reader = new StreamReader(fileStream);

        Path = await reader.ReadToEndAsync();
    }

    public ICommand GotoOnboardingCommand => new Command(async () =>
    {
        await AppShell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
    });

    public ICommand PrintAnimeCount => new Command(async () =>
    {
        //File.Delete(Globals.Location.Database);


        await realm.WriteAsync(() =>
        {
            var anime = new ContentDTO
            {
                Type = ContentType.TV.ToStringCached(),
                Title = "Paolo"
            };
            realm.Add(anime);
        });

        ObjectCount = realm.All<ContentDTO>().Count().ToString();
    });

    public ICommand DeleteRealmFile => new Command(() =>
    {
        realm.Dispose();
        File.Delete(Globals.Location.Database);
        realm = RealmAccess.Singleton.GetInstance(App.DatabaseConfiguration);
        ObjectCount = realm.All<ContentDTO>().Count().ToString();
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