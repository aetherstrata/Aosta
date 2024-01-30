global using NUnit.Framework;
global using FluentAssertions;

using Serilog;

namespace Aosta.Data.Tests;

[SetUpFixture]
[Parallelizable(ParallelScope.Children)]
public class GlobalSetup
{
    [OneTimeSetUp]
    public void Initialize()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Level:u}] {Message:lj} <{ThreadId}>{NewLine}{Exception}")
            .Enrich.WithThreadId()
            .CreateLogger();
    }
}
