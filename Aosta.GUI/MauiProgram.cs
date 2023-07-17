using System.Text;
using Aosta.Common.Consts;
using Aosta.GUI.Extensions;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using Serilog;
using Serilog.Events;

namespace Aosta.GUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        ConfigureSerilog();

        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseDevExpress()
            .RegisterFonts()
            .RegisterHandlers()
            .RegisterServices()
            .Logging.AddSerilog(Log.Logger, true);

        return builder.Build();
    }

    private static void ConfigureSerilog()
    {
        Log.Logger =  new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Verbose()
#else
            .MinimumLevel.Debug()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.FromLogContext()
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: Logging.OutputTemplate)
#if ANDROID
            .WriteTo.AndroidLog(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: Logging.OutputTemplate)
            .Enrich.WithProperty(Serilog.Core.Constants.SourceContextPropertyName, ".dev.aest.aosta")
#endif
#if DEBUG
            .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: Logging.OutputTemplate)
#endif
            .WriteTo.Async(sink => sink.File(path: Path.Combine(Path.Combine(FileSystem.Current.AppDataDirectory, "logs"), "aosta-.log"), restrictedToMinimumLevel: LogEventLevel.Debug,
                buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(1), encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7, outputTemplate: Logging.OutputTemplate))
            .CreateLogger();
    }
}