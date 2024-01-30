using System;
using System.IO;

using Android.App;
using Android.Content.PM;

using Aosta.Data;

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
            .UseAndroid()
            .UseReactiveUI()
            .LogToTrace()
            .AfterSetup(_ =>
            {
                // The default current directory on android is '/'.
                // On some devices '/' maps to the app data directory. On others it maps to the root of the internal storage.
                // In order to have a consistent current directory on all devices the full path of the app data directory is set as the current directory.
                Environment.CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                ILogger logger = App.SetupLogger(Path.Combine(Environment.CurrentDirectory, "logs"))
                                    .WriteTo.Logcat("dev.aest.aosta", "{Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}", LogEventLevel.Debug)
                                    .CreateLogger();

                Locator.CurrentMutable
                    .RegisterConstantAnd(logger)
                    .Register(() => new RealmAccess(Path.Combine(Environment.CurrentDirectory, "aosta.realm"), logger));
            });
    }
}
