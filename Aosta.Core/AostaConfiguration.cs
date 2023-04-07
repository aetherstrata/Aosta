using Realms;
using Serilog;
using Serilog.Events;
using System.Text;
using Aosta.Core.Jikan;

namespace Aosta.Core;

/// <summary> Aosta client configuration </summary>
public class AostaConfiguration
{
    private string _databasePath;
    private string _logPath;
    private string _cachePath;
    private IJikan _jikan = null!;
    private ILogger _logger;
    private RealmConfiguration _realmConfig = null!;

    /// <summary> Access the directory builder </summary>
    public AostaDirectoryBuilder With => new(this);

    /// <summary> Access the logger builder </summary>
    public AostaLoggerBuilder Log => new(this);

    /// <summary> Access the sources builder </summary>
    public AostaSourceBuilder Source => new(this);

    ///<summary> Log filename template </summary>
    private const string LogFilename = "aosta-.log";

    ///<summary> Output template </summary>
    private const string OutputTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] {Message:lj} <{ThreadId}><{ThreadName}>{NewLine}{Exception}";

    /// <summary> Get a copy of the Serilog logger configuration </summary>
    internal static LoggerConfiguration GetLoggerConfig(string logPath) => new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .Enrich.FromLogContext()
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: OutputTemplate)
#if DEBUG
        .WriteTo.Debug(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: OutputTemplate)
#endif
        .WriteTo.Async(sink => sink.File(path: Path.Combine(logPath, LogFilename), restrictedToMinimumLevel: LogEventLevel.Debug,
            buffered: true, flushToDiskInterval: TimeSpan.FromSeconds(1), encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 7, outputTemplate: OutputTemplate));

    internal AostaConfiguration() : this(AppContext.BaseDirectory)
    {
    }

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
        _cachePath = Path.Combine(dataPath, "cache");

        _logger = GetLoggerConfig(_logPath).CreateLogger();
    }

    public AostaConfiguration WithDatabaseDirectory(string dbPath)
    {
        _databasePath = dbPath;
        return this;
    }
    
    public AostaConfiguration WithLogDirectory(string logPath)
    {
        _logPath = logPath;
        return this;
    }

    public AostaDotNet Build()
    {
        _realmConfig = new RealmConfiguration(_databasePath)
        {
            SchemaVersion = 2,
            IsReadOnly = false,
            ShouldDeleteIfMigrationNeeded = true
        };

        _jikan ??= new JikanConfiguration().Build();

        return new AostaDotNet
        {
            Jikan = _jikan,
            Log = _logger,
            RealmConfig = _realmConfig
        };
    }

    public class AostaDirectoryBuilder
    {
        private readonly AostaConfiguration _aosta;

        internal AostaDirectoryBuilder(AostaConfiguration aosta)
        {
            _aosta = aosta;
        }
        
        public AostaConfiguration CacheDirectory(string cachePath)
        {
            _aosta._cachePath = cachePath;
            return _aosta;
        }
    }

    public class AostaLoggerBuilder
    {
        private readonly AostaConfiguration _aosta;

        internal AostaLoggerBuilder(AostaConfiguration aosta)
        {
            _aosta = aosta;
        }

        public AostaConfiguration With(LoggerConfiguration logConfig)
        {
            _aosta._logger = logConfig.CreateLogger();
            return _aosta;
        }

        public AostaConfiguration With(ILogger logger)
        {
            _aosta._logger = logger;
            return _aosta;
        }
    }

    public class AostaSourceBuilder : AostaConfiguration
    {
        private readonly AostaConfiguration _aosta;

        internal AostaSourceBuilder(AostaConfiguration aosta)
        {
            _aosta = aosta;
        }

        public AostaConfiguration From(IJikan jikan)
        {
            _aosta._jikan = jikan;
            return _aosta;
        }

        public AostaConfiguration From(JikanConfiguration jikanConfig)
        {
            _aosta._jikan = jikanConfig.Use.Logger(_logger).Build();
            return _aosta;
        }
    }
}