using Realms;
using Serilog;
using Serilog.Events;
using System.Text;
using Aosta.Common.Consts;

namespace Aosta.Core;

/// <summary> Aosta client configuration </summary>
public class AostaConfiguration
{
    private readonly string _databasePath;
    private readonly string _logPath;

    private ILogger _logger = null!;

    /// <summary> Access the logger builder </summary>
    public AostaServicesBuilder With => new(this);

    ///<summary> Log filename template </summary>
    private const string log_filename = "aosta-.log";

    /// <summary> Get a copy of the Serilog logger configuration </summary>
    public static LoggerConfiguration GetDefaultLoggerConfig(string logPath) => new LoggerConfiguration()
#if DEBUG
        .MinimumLevel.Verbose()
        .WriteTo.Console(outputTemplate: Logging.OUTPUT_TEMPLATE)
        .WriteTo.Debug(outputTemplate: Logging.OUTPUT_TEMPLATE)
#else
        .MinimumLevel.Debug()
#endif
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.FromLogContext()
        .WriteTo.Async(sink => sink.File(path: Path.Combine(logPath, log_filename),
            restrictedToMinimumLevel: LogEventLevel.Debug,
            buffered: true,
            flushToDiskInterval: TimeSpan.FromSeconds(1),
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 7,
            outputTemplate: Logging.OUTPUT_TEMPLATE));

    public AostaConfiguration(string dataDir)
    {
        string dataPath;

        try
        {
            dataPath = Path.GetFullPath(dataDir);
        }
        catch (Exception)
        {
            Console.WriteLine("Provided path is not valid!");
            throw;
        }

        try
        {
            Directory.CreateDirectory(dataPath);
        }
        catch (Exception)
        {
            Console.WriteLine("Could not create application folder!");
            throw;
        }

        _databasePath = Path.Combine(dataPath, "aosta.realm");
        _logPath = Path.Combine(dataPath, "logs");
        Path.Combine(dataPath, "cache");
    }

    public AostaDotNet Build()
    {
        _logger ??= GetDefaultLoggerConfig(_logPath).CreateLogger();

        var realmConfig = new RealmConfiguration(_databasePath)
        {
            SchemaVersion = 2,
            IsReadOnly = false,
#if DEBUG
            ShouldDeleteIfMigrationNeeded = true
#else
            ShouldDeleteIfMigrationNeeded = false
#endif
        };

        return new AostaDotNet(_logger, realmConfig);
    }

    public class AostaServicesBuilder
    {
        private readonly AostaConfiguration _aosta;

        internal AostaServicesBuilder(AostaConfiguration aosta)
        {
            _aosta = aosta;
        }

        public AostaConfiguration Logger(LoggerConfiguration logConfig)
        {
            _aosta._logger = logConfig.CreateLogger();
            return _aosta;
        }

        public AostaConfiguration Logger(ILogger logger)
        {
            _aosta._logger = logger;
            return _aosta;
        }
    }
}
