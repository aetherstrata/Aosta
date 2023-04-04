using Aosta.Core.Jikan;

namespace Aosta.Core.Tests.Jikan;

[SetUpFixture]
public class JikanSetup
{
    public static IJikan Instance { get; private set; } = new JikanClient();

    [OneTimeSetUp]
    public void Initialize()
    {
    }
}