// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Reflection;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.Models;
using Aosta.Ava.ViewModels;
using Aosta.Common.Extensions;
using Aosta.Core;
using Aosta.Jikan;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

using ReactiveUI;

using Splat;
using Splat.Serilog;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava;

public partial class App : Application
{
    public const string Version = "0.0.1";

    private ILogger _logger = null!;

    public override void Initialize()
    {
        DataTemplates.Add(new FuncDataTemplate<ILocalized>(_ => true,
            _ => new TextBlock
            {
                [!TextBlock.TextProperty] = new Binding(nameof(ILocalized.Localized))
            }));

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _logger = Locator.Current.GetSafely<ILogger>();

        Locator.CurrentMutable.UseSerilogFullLogger(_logger);

        Locator.CurrentMutable
            .RegisterAnd(() => new JikanConfiguration()
                .Use.Logger(_logger)
                .Build())
            .RegisterAnd(() => Locator.Current.GetSafely<AostaDotNet>().Realm)
            .RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        var language = LanguageKey.Load().OrDefault(LanguageKey.DEFAULT).Language;
        Localizer.Instance.Language = language;
        _logger.Information("Loaded language {LanguageCode}", language.GetLanguageCode());

        var themeKey = ThemeKey.Load().OrDefault(ThemeKey.DEFAULT);
        RequestedThemeVariant = themeKey.Theme;
        _logger.Information("Loaded theme {Variant}", themeKey.Key);

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
}
