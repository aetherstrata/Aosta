using Realms;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Text;

namespace Aosta.Core
{
    /// <summary> Aosta client configuration </summary>
    public class AostaConfiguration
    {
        /// <summary> Data location </summary>
        public string AppDataPath { get; }

        /// <summary> Realm database location </summary>
        public string DatabasePath { get; }

        /// <summary> Logs folder location </summary>
        public string LogPath { get; }

        /// <summary> Control the minimum log level </summary>
        internal LoggingLevelSwitch LoggerLevelSwitch { get; } = new();

        /// <summary> Serilog configuration </summary>
        internal LoggerConfiguration LoggerConfig { get; }

        /// <summary> Realm configuration </summary>
        internal RealmConfiguration RealmConfig { get; }

        internal AostaConfiguration(string appdata)
        {
            try
            {
                AppDataPath = Path.GetFullPath(appdata);
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

            LoggerLevelSwitch.MinimumLevel = LogEventLevel.Debug;

            LoggerConfig = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(LoggerLevelSwitch)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .Enrich.FromLogContext()
                .WriteTo.Console()
#if DEBUG
                .WriteTo.Debug(LogEventLevel.Verbose)
#endif
                .WriteTo.Async(a => a.File(LogPath, buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(1), encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}"));

            RealmConfig = new RealmConfiguration(DatabasePath)
            {
                SchemaVersion = 2,
                IsReadOnly = false,
                ShouldDeleteIfMigrationNeeded = true
            };
        }
    }
}
