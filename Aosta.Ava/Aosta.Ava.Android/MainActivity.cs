using System;
using System.IO;

using Android.App;
using Android.Content.PM;

using Aosta.Ava.Extensions;
using Aosta.Common.Consts;
using Aosta.Common.Extensions;
using Aosta.Core;

using Avalonia;
using Avalonia.Android;
using Avalonia.ReactiveUI;

using Serilog.Events;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava.Android;

[Activity(
    Label = "AOSTA",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .UseReactiveUI()
            .LogToTrace()
            .AfterSetup(_ =>
            {
                Locator.CurrentMutable.RegisterAnd<ILogger>(() => AostaConfiguration
                    .GetDefaultLoggerConfig(Path.Combine(global::Android.App.Application.Context.FilesDir.AsNonNull().Path, "logs"))
                    .WriteTo.Logcat("AOSTA", Logging.OUTPUT_TEMPLATE, LogEventLevel.Debug)
                    .CreateLogger())
                    .Register(() =>  new AostaConfiguration(Environment.GetFolderPath(Environment.SpecialFolder.Personal))
                        .With.Logger(Locator.Current.GetSafely<ILogger>())
                        .Build());
            });
    }
}
