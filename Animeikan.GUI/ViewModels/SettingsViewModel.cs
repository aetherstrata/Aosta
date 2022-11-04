using Animeikan.GUI.Data;
using Animeikan.GUI.Models;
using Animeikan.GUI.Services;
using Animeikan.GUI.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using JikanDotNet;

using Realms;

using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
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
  private string _anime = "AHA";

  [ObservableProperty]
  private bool _darkModeSwitch = Application.Current.UserAppTheme.HasFlag(AppTheme.Dark);

  public SettingsViewModel(ISettingsService settingsService, IJikan jikan)
  {
    this.jikan = jikan;
    this.settingsService = settingsService;
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

  public ICommand PrintAnime => new Command(async () =>
  {
    //TODO
    var test = await jikan.GetAnimeAsync(1);

    var pp = test.Data;

    AnimeData dio = new AnimeData()
    {
      Title = "PORCODIO"
    };

    using (var db = RealmInstance.Manager.Db)
    {
      await db.WriteAsync(() =>
      {
        db.Add(dio);
      });

      Anime = db.Find<AnimeData>(dio.Id).Title;
    }
  });

  public ICommand UpdateTheme => new Command(async () =>
  {
    if (DarkModeSwitch)
    {
      await settingsService.Save<bool>("useDarkTheme", true);
      Application.Current.UserAppTheme = AppTheme.Dark;
    }
    else
    {
      await settingsService.Save<bool>("useDarkTheme", false);
      Application.Current.UserAppTheme = AppTheme.Light;
    }
  });
}

