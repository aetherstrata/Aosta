using Animeikan.GUI.Models;
using Animeikan.GUI.Services;
using Animeikan.GUI.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using JikanDotNet;

using Microsoft.Maui.Controls;

using Realms;

using System.Windows.Input;

namespace Animeikan.GUI.ViewModels;

[ObservableObject]
public partial class SettingsViewModel
{
  IJikan jikan;
  ISettingsService settingsService;

  [ObservableProperty]
  private string _path = Globals.Location.AppData;

  [ObservableProperty]
  private string _objectCount = "N/A";

  [ObservableProperty]
  private string _anime = "AHA";

  [ObservableProperty]
  private bool _darkModeSwitch = Application.Current?.UserAppTheme == AppTheme.Dark;

  public SettingsViewModel(ISettingsService settingsService, IJikan jikan)
  {
    this.jikan = jikan;
    this.settingsService = settingsService;

    using (var db = Realm.GetInstance(Globals.RealmConfig.Default))
    {
      ObjectCount = db.All<AnimeModel>().Count().ToString();
    }
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

    using (var db = Realm.GetInstance(Globals.RealmConfig.Default))
    {
      await db.WriteAsync(() =>
      {
        var anime = new AnimeModel() { Title = "Paolo"};
        db.Add(anime);
      });

      ObjectCount = db.All<AnimeModel>().Count().ToString();
    }
  });

  public ICommand DeleteRealmFile => new Command(() =>
  {
    File.Delete(Globals.Location.Database);

    using (var db = Realm.GetInstance(Globals.RealmConfig.Default))
    {
      ObjectCount = db.All<AnimeModel>().Count().ToString();
    }
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

