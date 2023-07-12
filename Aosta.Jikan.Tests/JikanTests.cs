using Aosta.Common.Consts;
using Serilog;
using Serilog.Events;

namespace Aosta.Jikan.Tests;

[SetUpFixture]
public class JikanTests
{
    public static IJikan Instance { get; private set; } = new JikanConfiguration()
        .Use.Logger(new LoggerConfiguration()
            .MinimumLevel.Is(LogEventLevel.Debug)
            .WriteTo.Console(outputTemplate: Logging.OutputTemplate)
            .CreateLogger())
        .Build();

    [OneTimeSetUp]
    public void Initialize()
    {
    }
}