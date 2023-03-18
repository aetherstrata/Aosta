using Aosta.Core.Data.Enums;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class WatchStatusTests : IEnumTests
{
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That((int)WatchStatus.PlanToWatch, Is.EqualTo(-1));
            Assert.That((int)WatchStatus.Dropped, Is.EqualTo(0));
            Assert.That((int)WatchStatus.Completed, Is.EqualTo(1));
            Assert.That((int)WatchStatus.OnHold, Is.EqualTo(2));
            Assert.That((int)WatchStatus.Watching, Is.EqualTo(3));
        });
    }

    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<WatchStatus>(), Has.Length.EqualTo(5));
    }

    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(WatchStatus.PlanToWatch.ToStringCached(), Is.EqualTo("Plan to watch"));
            Assert.That(WatchStatus.Dropped.ToStringCached(), Is.EqualTo("Dropped"));
            Assert.That(WatchStatus.Completed.ToStringCached(), Is.EqualTo("Completed"));
            Assert.That(WatchStatus.OnHold.ToStringCached(), Is.EqualTo("On Hold"));
            Assert.That(WatchStatus.Watching.ToStringCached(), Is.EqualTo("Watching"));
        });
    }
}