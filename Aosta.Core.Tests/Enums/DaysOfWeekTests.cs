using Aosta.Core.Data.Enums;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class DaysOfWeekTests : IEnumTests
{
    [Test]
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That((int)DaysOfWeek.None, Is.EqualTo(0));
            Assert.That((int)DaysOfWeek.Monday, Is.EqualTo(1));
            Assert.That((int)DaysOfWeek.Tuesday, Is.EqualTo(2));
            Assert.That((int)DaysOfWeek.Wednesday, Is.EqualTo(4));
            Assert.That((int)DaysOfWeek.Thursday, Is.EqualTo(8));
            Assert.That((int)DaysOfWeek.Friday, Is.EqualTo(16));
            Assert.That((int)DaysOfWeek.Saturday, Is.EqualTo(32));
            Assert.That((int)DaysOfWeek.Sunday, Is.EqualTo(64));
        });
    }

    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<DaysOfWeek>(), Has.Length.EqualTo(8));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(DaysOfWeek.None.ToStringCached(), Is.EqualTo("None"));
            Assert.That(DaysOfWeek.Monday.ToStringCached(), Is.EqualTo("Mondays"));
            Assert.That(DaysOfWeek.Tuesday.ToStringCached(), Is.EqualTo("Tuesdays"));
            Assert.That(DaysOfWeek.Wednesday.ToStringCached(), Is.EqualTo("Wednesdays"));
            Assert.That(DaysOfWeek.Thursday.ToStringCached(), Is.EqualTo("Thursdays"));
            Assert.That(DaysOfWeek.Friday.ToStringCached(), Is.EqualTo("Fridays"));
            Assert.That(DaysOfWeek.Saturday.ToStringCached(), Is.EqualTo("Saturdays"));
            Assert.That(DaysOfWeek.Sunday.ToStringCached(), Is.EqualTo("Sundays"));
        });
    }

    [Test]
    public void JikanStringParseTest()
    {
        var converted = new AnimeBroadcast { Day = "Mondays" }.ToRealmObject();
        Assert.That(converted.Day, Is.EqualTo(DaysOfWeek.Monday));
    }

    [Test]
    public void JikanInvalidStringParseTest()
    {
        var converted = new AnimeBroadcast { Day = "Invalid" }.ToRealmObject();
        Assert.That(converted.Day, Is.EqualTo(DaysOfWeek.None));
    }

    [Test]
    public void JikanNullStringParseTest()
    {
        var converted = new AnimeBroadcast { Day = null }.ToRealmObject();
        Assert.That(converted.Day, Is.EqualTo(DaysOfWeek.None));
    }
}