using System.Diagnostics;
using System.Windows.Input;
using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models;
using Aosta.GUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using JikanDotNet;
using Realms;
using AiringStatus = Aosta.Core.Data.Enums.AiringStatus;
using Location = Aosta.GUI.Globals.Location;

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
    private string _path = Location.AppData;

    private IJikan jikan;

    private int count = 1;

    private readonly ISettingsService settingsService;

    public SettingsViewModel(ISettingsService settingsService, IJikan jikan)
    {
        this.jikan = jikan;
        this.settingsService = settingsService;

        UpdateRealmCount();
    }

    public ICommand GotoOnboardingCommand => new Command(async () =>
    {
        await Shell.Current.GoToAsync($"{nameof(OnboardingPage.OnboardingPage)}");
    });

    public ICommand PrintAnimeCount => new Command(async () =>
    {
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            await App.Core.CreateJikanContentAsync(count);

            await Realm.WriteAsync(() =>
            {
                Realm.Add(new ContentObject()
                {
                    Title = "Pippo " + count,
                    JikanResponseData = Realm.Find<JikanContentObject>(count)
                });
            });

            count++;
            ObjectCount = Realm.All<ContentObject>().Count().ToString();
        }
        else
        {
            Debug.WriteLine("No internet! Jikan will fail...");
        }
    });

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
            await settingsService.Save("useDarkTheme", true);
        }
        else
        {
            Application.Current!.UserAppTheme = AppTheme.Light;
            await settingsService.Save("useDarkTheme", false);
        }
    });

    public void UpdateRealmCount() => ObjectCount = Realm.All<ContentObject>().Count().ToString();

    public async Task LoadAssetToString(string fileName)
    {
        using var fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
        using var reader = new StreamReader(fileStream);

        Path = await reader.ReadToEndAsync();
    }
}