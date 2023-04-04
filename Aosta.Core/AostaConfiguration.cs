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
    public string AppDataPath { get; }

    /// <summary> Realm database location </summary>
    public string DatabasePath { get; }

    /// <summary> Logs folder location </summary>
    public string LogPath { get; }

    /// <summary> Cache folder location </summary>
    public string CachePath { get; init; }

    /// <summary> Jikan configuration </summary>
    public JikanConfiguration JikanConfig { get; init; }

    ///<summary> Output template </summary>
    private const string OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}";

    /// <summary> Get a copy of the Serilog logger configuration </summary>
    internal LoggerConfiguration LoggerConfig => new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.FromLogContext()
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: OutputTemplate)
#if DEBUG
        .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: OutputTemplate)
#endif
        .WriteTo.Async(sink => sink.File(path: LogPath, restrictedToMinimumLevel: LogEventLevel.Debug, buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(1),
            encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7, outputTemplate: OutputTemplate));

    /// <summary> Realm configuration </summary>
    internal RealmConfiguration RealmConfig { get; }

    internal AostaConfiguration() : this(AppContext.BaseDirectory)
    {
    }

    public AostaConfiguration(string dataDir)
    {
        try
        {
            AppDataPath = Path.GetFullPath(dataDir);
        }
        catch (Exception)
        {
            Console.WriteLine("Provided path is not valid!");
            throw;
        }

        try
        {
            Directory.CreateDirectory(AppDataPath);
        }
        catch (Exception)
        {
            Console.WriteLine("Could not create application folder!");
            throw;
        }

        DatabasePath = Path.Combine(AppDataPath, "aosta.realm");
        LogPath = Path.Combine(AppDataPath, "logs", "aosta-.log");
        CachePath = Path.Combine(AppDataPath, "cache");

        RealmConfig = new RealmConfiguration(DatabasePath)
        {
            SchemaVersion = 2,
            IsReadOnly = false,
            ShouldDeleteIfMigrationNeeded = true
        };

        JikanConfig = new JikanConfiguration();
    }
}