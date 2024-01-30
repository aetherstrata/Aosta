//Everything in this project was mostly written by the original creator of Jikan.NET

using Serilog;

namespace Aosta.Jikan.Tests;

[SetUpFixture]
public class JikanTests
{
    public static IJikan Instance { get; private set; }

    [OneTimeSetUp]
    public void Initialize()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Level:u}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        Instance = new JikanConfiguration()
            .Use.Logger(Log.Logger)
            .Build();
    }
}
