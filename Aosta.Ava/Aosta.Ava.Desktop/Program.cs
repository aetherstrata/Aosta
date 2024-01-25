using System;
using System.IO;

using Aosta.Ava.Extensions;
using Aosta.Core;

using Avalonia;
using Avalonia.ReactiveUI;

using Splat;

using ILogger = Serilog.ILogger;

namespace Aosta.Ava.Desktop;

internal static class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI()
            .AfterSetup(_ =>
            {
                Locator.CurrentMutable
                    .RegisterAnd<ILogger>(() => AostaConfiguration
                        .GetDefaultLoggerConfig(Path.Combine(AppContext.BaseDirectory, "logs"))
                        .CreateLogger())
                    .Register(() => new AostaConfiguration(AppContext.BaseDirectory)
                        .With.Logger(Locator.Current.GetSafely<ILogger>())
                        .Build());
            });
}
