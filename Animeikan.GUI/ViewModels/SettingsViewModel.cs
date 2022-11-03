using Animeikan.GUI.Data;
using Animeikan.GUI.Models;
using Animeikan.GUI.Services;
using Animeikan.GUI.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using Realms;

using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Animeikan.GUI.ViewModels;

[ObservableObject]
public partial class SettingsViewModel
{
  [ObservableProperty]
  private string _path;

  [ObservableProperty]
  private string _anime = "AHA";

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
    AnimeData dio = new AnimeData()
    {
      Title = "PORCODIO"
    };

    Realm db = RealmInstance.Singleton.Db;

    await db.WriteAsync(() =>
    {
      db.Add(dio);
    });

    Anime = db.Find<AnimeData>(dio.Id).Title;
  });
}

