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
using Aosta.Jikan;

using Avalonia;
using Avalonia.Controls;
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

    public const string VERSION = "0.0.1";

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _logger = Locator.Current.GetSafely<ILogger>();

        configureLogging(_logger);

        Locator.CurrentMutable
            .RegisterAnd(() => new JikanConfiguration()
                .Use.Logger(_logger)
                .Build())
            .RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

        var language = LanguageKey.Load().Language;
        Localizer.Instance.Language = language;
        _logger.Information("Loaded language {LanguageCode}", language.GetLanguageCode());

        var themeKey = ThemeKey.Load();
        RequestedThemeVariant = themeKey.Theme;
        _logger.Information("Loaded theme {Variant}", themeKey.LocalizationKey);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var ctx = new MainViewModel();

            desktop.MainWindow = new MainWindow
            {
                DataContext = ctx
            };

            if (!Design.IsDesignMode)
            {
                DataContext = ctx;
            }
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var ctx = new MainViewModel();

            singleViewPlatform.MainView = new MainView
            {
                DataContext = ctx
            };

            if (!Design.IsDesignMode)
            {
                DataContext = ctx;
            }
        }

        _logger.Debug("Framework initialization completed");

        base.OnFrameworkInitializationCompleted();
    }

    private static void configureLogging(ILogger logger)
    {
        //Set logger as static default instance
        Log.Logger = logger;

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
            logger.Write(serilogLevel, message);
        });

        //Set ReactiveUI logger
        Locator.CurrentMutable.UseSerilogFullLogger(logger);

        //Set Avalonia logger
        Avalonia.Logging.Logger.Sink = new SerilogSink(logger);

        //Log unhandled exceptions
        AppDomain.CurrentDomain.UnhandledException += static (sender, args) =>
        {
            Log.Fatal((Exception)args.ExceptionObject, "An unhandled exception was thrown by {Sender}", sender.GetType());
        };
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
