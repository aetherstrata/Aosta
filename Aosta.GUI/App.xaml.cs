using Aosta.GUI.Features.OnboardingPage;

using Aosta.GUI.Services;

namespace Aosta.GUI;

public partial class App
{
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