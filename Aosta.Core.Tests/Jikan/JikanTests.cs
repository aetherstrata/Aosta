using Aosta.Core.Jikan;
using Serilog;

namespace Aosta.Core.Tests.Jikan;

[SetUpFixture]
public class JikanTests
{
    public static IJikan Instance { get; private set; } = new JikanConfiguration().Use.Logger(Log.Logger).Build();

    [OneTimeSetUp]
    public void Initialize()
    {
    }
}