using System;
using System.IO;

using Aosta.Data;

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
            .UseReactiveUI()
            .LogToTrace()
            .AfterSetup(_ =>
            {
                ILogger logger = App.SetupLogger(Path.Combine(AppContext.BaseDirectory, "logs"))
                                    .CreateLogger();

                Locator.CurrentMutable
                    .RegisterConstantAnd(logger)
                    .RegisterConstant(new RealmAccess(Path.Combine(AppContext.BaseDirectory, "aosta.realm"), logger));
            });
}
