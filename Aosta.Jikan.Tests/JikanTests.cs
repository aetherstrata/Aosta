using Serilog;

namespace Aosta.Jikan.Tests;

[SetUpFixture]
public class JikanTests
{
    public static IJikan Instance { get; private set; } = new JikanConfiguration().Use.Logger(Log.Logger).Build();

    [OneTimeSetUp]
    public void Initialize()
    {
    }
}