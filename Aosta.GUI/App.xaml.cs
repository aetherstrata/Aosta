using Aosta.Core;
using Aosta.Core.Jikan;
using Aosta.Core.Limiter;
using Aosta.GUI.Features.OnboardingPage;
using Aosta.GUI.Services;
using Realms;
using Location = Aosta.GUI.Globals.Location;

namespace Aosta.GUI;

public partial class App : Application
{
    public static readonly AostaDotNet Core = new(new RealmConfiguration(Location.Database)
    {
        SchemaVersion = 2,
        IsReadOnly = false,
        ShouldDeleteIfMigrationNeeded = true
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

        if (_settingsService.Get("firstRun", true).Result)
            Shell.Current.GoToAsync($"{nameof(OnboardingPage)}");
    }
}