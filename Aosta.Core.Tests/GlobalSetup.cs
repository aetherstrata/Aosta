using Serilog;

namespace Aosta.Core.Tests;

[SetUpFixture]
[Parallelizable(ParallelScope.Children)]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void Initialize()
    {
        Log.Logger = AostaConfiguration.GetLoggerConfig(AppContext.BaseDirectory).CreateLogger();
    }
}