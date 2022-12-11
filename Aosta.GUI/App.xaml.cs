using Aosta.Core;
using Aosta.Core.Realm;
using Aosta.GUI.Services;
using Realms;

namespace Aosta.GUI;

public partial class App : Application
{
    internal static readonly DatabaseConfiguration DatabaseConfiguration = new()
    {
        Location = Globals.Location.Database,
        Configuration = new(Globals.Location.Database)
        {
            IsReadOnly = false,
            ShouldDeleteIfMigrationNeeded = true
        }
    };

    public static AostaDotNet Core = new AostaDotNet()
    {
        DatabaseConfiguration = DatabaseConfiguration
    };

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