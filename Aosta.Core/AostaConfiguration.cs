using Realms;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Text;
using Aosta.Core.Jikan;

namespace Aosta.Core;

/// <summary> Aosta client configuration </summary>
public class AostaConfiguration
{
    /// <summary> Data location </summary>
    public string AppDataPath { get; init; }

    /// <summary> Realm database location </summary>
    public string DatabasePath { get; init; }

    /// <summary> Logs folder location </summary>
    public string LogPath { get; init; }

    /// <summary> Cache folder location </summary>
    public string CachePath { get; init; }

    /// <summary> Control the minimum log level </summary>
    public LoggingLevelSwitch LoggerLevelSwitch { get; } = new();

    /// <summary> Get a copy of the Serilog logger configuration </summary>
    internal LoggerConfiguration LoggerConfig => new LoggerConfiguration()
        .MinimumLevel.ControlledBy(LoggerLevelSwitch)
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.FromLogContext()
        .WriteTo.Console(LogEventLevel.Debug)
#if DEBUG
        .WriteTo.Debug(LogEventLevel.Verbose)
#endif
        .WriteTo.Async(a => a.File(LogPath, buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(1),
            encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7,
            outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}"));

    /// <summary> Realm configuration </summary>
    public RealmConfiguration RealmConfig { get; init; }

    /// <summary> Jikan configuration </summary>
    public JikanConfiguration JikanConfig { get; init; }

    internal AostaConfiguration() : this(AppContext.BaseDirectory)
    {
    }

    public AostaConfiguration(string dataDir)
    {
        try
        {
            AppDataPath = Path.GetFullPath(dataDir);
        }
        catch (Exception e)
        {
            Console.WriteLine("Provided path is not valid!");
            throw;
        }

        try
        {
            Directory.CreateDirectory(AppDataPath);
        }
        catch (Exception e)
        {
            Console.WriteLine("Could not create application folder!");
            throw;
        }

        DatabasePath = Path.Combine(AppDataPath, "aosta.realm");
        LogPath = Path.Combine(AppDataPath, "logs", "aosta-.log");
        CachePath = Path.Combine(AppDataPath, "cache");

        LoggerLevelSwitch.MinimumLevel = LogEventLevel.Verbose;

        RealmConfig = new RealmConfiguration(DatabasePath)
        {
            SchemaVersion = 2,
            IsReadOnly = false,
            ShouldDeleteIfMigrationNeeded = true
        };

        JikanConfig = new JikanConfiguration();
    }


}