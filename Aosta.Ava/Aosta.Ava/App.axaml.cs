// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

using Aosta.Ava.Extensions;
using Aosta.Ava.Localization;
using Aosta.Ava.Settings;
using Aosta.Ava.ViewModels;
using Aosta.Common.Extensions;
using Aosta.Jikan;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using ReactiveUI;

using Realms.Logging;

using Serilog;
using Serilog.Events;

using Splat;
using Splat.Serilog;

using ILogger = Serilog.ILogger;
using LogLevel = Realms.Logging.LogLevel;

namespace Aosta.Ava;

public partial class App : Application
{
    private ILogger _logger = null!;

    public const string Version = "0.0.1";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _logger = Locator.Current.GetSafely<ILogger>();

        configureLogging();

        Locator.CurrentMutable
            .RegisterAnd(() => new JikanConfiguration()
                .Use.Logger(_logger)
                .Build())
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

    private void configureLogging()
    {
        //Set Realm logger
        Logger.LogLevel = LogLevel.Debug;
        Logger.Default = Logger.Function((level, message) =>
        {
            if (level == LogLevel.Off) return;

            var serilogLevel = level switch
            {
                LogLevel.All => LogEventLevel.Verbose,
                LogLevel.Trace => LogEventLevel.Verbose,
                LogLevel.Debug => LogEventLevel.Debug,
                LogLevel.Detail => LogEventLevel.Debug,
                LogLevel.Info => LogEventLevel.Information,
                LogLevel.Warn => LogEventLevel.Warning,
                LogLevel.Error => LogEventLevel.Error,
                LogLevel.Fatal => LogEventLevel.Fatal,
                _ => throw new UnreachableException()
            };

            // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
            _logger.Write(serilogLevel, message);
        });

        //Set ReactiveUI logger
        Locator.CurrentMutable.UseSerilogFullLogger(_logger);

        Avalonia.Logging.Logger.Sink = new SerilogSink(_logger);
    }

    /// Get a copy of the Serilog logger configuration
    public static LoggerConfiguration SetupLogger(string logPath) =>
        new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Verbose()
            .WriteTo.Console(outputTemplate: log_template)
            .WriteTo.Debug(outputTemplate: log_template)
#else
            .MinimumLevel.Debug()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.FromLogContext()
            .WriteTo.Async(sink => sink.File(
                path: Path.Combine(logPath, "aosta-.log"),
                restrictedToMinimumLevel: LogEventLevel.Debug,
                buffered: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1),
                encoding: Encoding.UTF8,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: log_template));

    private const string log_template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] {Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}";
}
