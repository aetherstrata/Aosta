using Aosta.Core;
using Aosta.Core.Jikan;
using Aosta.Core.Limiter;
using Aosta.GUI.Services;

namespace Aosta.GUI;

public partial class App
{
    public static readonly AostaDotNet Core = new(new AostaConfiguration(FileSystem.Current.AppDataDirectory)
    {
        CachePath = FileSystem.Current.CacheDirectory,
        JikanConfig = new JikanConfiguration()
        {
            LimiterConfigurations = TaskLimiterConfiguration.Default
        }
    });

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