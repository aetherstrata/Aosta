using Animeikan.GUI.Services;

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
      UserAppTheme = AppTheme.Dark;
    else
      UserAppTheme = AppTheme.Light;

    if (!settingsService.Get<bool>("firstRun", true).Result)
      AppShell.Current.GoToAsync($"//{nameof(MainPage)}");
  }
}
