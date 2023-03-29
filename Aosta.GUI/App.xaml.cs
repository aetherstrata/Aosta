using Aosta.Core;
using Aosta.GUI.Services;

namespace Aosta.GUI;

public partial class App : Application
{
    public static readonly Core.AostaDotNet Core = new Core.AostaDotNet(FileSystem.Current.AppDataDirectory);

    private readonly ISettingsService _settingsService;

    public App(ISettingsService settingsService)
    {
        InitializeComponent();

        _settingsService = settingsService;

        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        UserAppTheme = _settingsService.Get("useDarkTheme", true).Result
            ? AppTheme.Dark
            : AppTheme.Light;

        if (!_settingsService.Get("firstRun", true).Result)
            Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}