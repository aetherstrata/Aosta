using System.Reflection;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.ViewModels;
using Aosta.Ava.ViewModels.Settings;
using Aosta.Core;
using Aosta.Core.Database;
using Aosta.Jikan;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using ReactiveUI;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava;

public partial class App : Application
{
    public const string Version = "0.0.1";

    private ILogger _logger = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _logger = Locator.Current.GetSafely<ILogger>();

        Locator.CurrentMutable
            .RegisterAnd(() => new JikanConfiguration()
                .Use.Logger(_logger)
                .Build())
            .RegisterAnd(() => Locator.Current.GetSafely<AostaDotNet>().Realm)
            .RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        Localizer.Instance.Language = InterfaceLanguage.English;

        setTheme();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        _logger.Debug("Framework initialization completed");

        base.OnFrameworkInitializationCompleted();
    }

    private void setTheme()
    {
        string? settingValue = Locator.Current
            .GetSafely<RealmAccess>()
            .GetSetting<string>(Settings.AppTheme);

        var key = settingValue is null ? ThemeKey.DEFAULT : (ThemeKey)settingValue;

        RequestedThemeVariant = key.Theme;

        _logger.Information("Loaded theme {Variant}", key.Theme.Key);
    }
}
