using Aosta.Core.Data.Enums;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;
using AiringStatus = Aosta.Core.Data.Enums.AiringStatus;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class AiringStatusTests : IEnumTests
{
    [Test]
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That((int)AiringStatus.NotAvailable, Is.EqualTo(-1));
            Assert.That((int)AiringStatus.Cancelled, Is.EqualTo(0));
            Assert.That((int)AiringStatus.FinishedAiring, Is.EqualTo(1));
            Assert.That((int)AiringStatus.NotYetAired, Is.EqualTo(2));
            Assert.That((int)AiringStatus.CurrentlyAiring, Is.EqualTo(3));
        });
    }

    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<AiringStatus>(), Has.Length.EqualTo(5));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(AiringStatus.NotAvailable.ToStringCached(), Is.EqualTo("Not available"));
            Assert.That(AiringStatus.Cancelled.ToStringCached(), Is.EqualTo("Cancelled"));
            Assert.That(AiringStatus.FinishedAiring.ToStringCached(), Is.EqualTo("Finished airing"));
            Assert.That(AiringStatus.NotYetAired.ToStringCached(), Is.EqualTo("Not yet aired"));
            Assert.That(AiringStatus.CurrentlyAiring.ToStringCached(), Is.EqualTo("Currently airing"));
        });
    }

    [Test]
    public void JikanStringParseTest()
    {
        var converted = new AnimeResponse() { MalId = 0, Status = "Not yet aired" }.ToRealmObject();
        Assert.That(converted.Status, Is.EqualTo(AiringStatus.NotYetAired));
    }

    [Test]
    public void JikanInvalidStringParseTest()
    {
        var converted = new AnimeResponse() { MalId = 0, Status = "Invalid" }.ToRealmObject();
        Assert.That(converted.Status, Is.EqualTo(AiringStatus.NotAvailable));
    }

    [Test]
    public void JikanNullStringParseTest()
    {
        var converted = new AnimeResponse() { MalId = 0, Status = null }.ToRealmObject();
        Assert.That(converted.Status, Is.EqualTo(AiringStatus.NotAvailable));
    }
}