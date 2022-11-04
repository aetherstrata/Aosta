using Animeikan.GUI.Services;
using Animeikan.GUI.Views;

using JikanDotNet;

namespace Animeikan.GUI;

public partial class App : Application
{
  ISettingsService settingsService;

  public App(ISettingsService settingsService)
  {
    InitializeComponent();

    this.settingsService = settingsService;

    MainPage = new AppShell();
  }
  
  protected override void OnStart()
  {
    if (settingsService.Get<bool>("useDarkTheme", true).Result)
      Application.Current.UserAppTheme = AppTheme.Dark;
    else
      Application.Current.UserAppTheme = AppTheme.Light;

    if (!settingsService.Get<bool>("firstRun", true).Result)
      AppShell.Current.GoToAsync($"//{nameof(MainPage)}");

  }
}
