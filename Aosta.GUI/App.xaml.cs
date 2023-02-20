using Aosta.Core;
using Aosta.GUI.Globals;
using Aosta.GUI.Services;
using Realms;

namespace Aosta.GUI;

public partial class App : Application
{
    public static readonly AostaDotNet Core = new(new RealmConfiguration(Globals.Location.Database)
    {
        SchemaVersion = 1,
        IsReadOnly = false,
        ShouldDeleteIfMigrationNeeded = true
    });

    private readonly ISettingsService _settingsService;

    public App(ISettingsService settingsService)
    {
        InitializeComponent();

        this._settingsService = settingsService;

        MainPage = new AppShell();
    }

    protected override void OnStart()
    {
        UserAppTheme = _settingsService.Get<bool>("useDarkTheme", true).Result
            ? AppTheme.Dark : AppTheme.Light;

        if (!_settingsService.Get<bool>("firstRun", true).Result)
            AppShell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}