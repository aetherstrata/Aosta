using Serilog;

namespace Aosta.Core.Tests;

[SetUpFixture]
[Parallelizable(ParallelScope.Fixtures)]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void Initialize()
    {
        Log.Logger = new AostaConfiguration().LoggerConfig.CreateLogger();
    }
}