using Animeikan.GUI.Services;
using Animeikan.GUI.Views;

using JikanDotNet;

namespace Animeikan.GUI;

public partial class App : Application
{
  ISettingsService settingsService;
  bool isFirstRun;

  public App(ISettingsService settingsService)
  {
    InitializeComponent();

    this.settingsService = settingsService;
    this.isFirstRun = settingsService.Get<bool>("firstRun", true).Result;

    MainPage = new AppShell();
  }
  
  protected override void OnStart()
  {
    if (!isFirstRun)
      AppShell.Current.GoToAsync($"//{nameof(MainPage)}");
  }

}
