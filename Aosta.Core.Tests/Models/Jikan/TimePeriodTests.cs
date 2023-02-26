using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class TimePeriodTests
{
    private DateTime _from;
    private DateTime _to;

    private TimePeriod _period = null!;

    [SetUp]
    public void SetUp()
    {
        _from = new DateTime(2000, 10, 24);
        _to = new DateTime(2001, 1, 8);

        _period = new()
        {
            From = _from,
            To = _to
        };
    }

    [Test]
    public void ConversionTest()
    {
        var converted = _period.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.From.HasValue, Is.True);
            Assert.That(converted.To.HasValue, Is.True);
        });

        Assert.Multiple(() =>
        {
            Assert.That(converted.From, Is.EqualTo(_period.From));
            Assert.That(converted.To, Is.EqualTo(_period.To));

            Assert.That(converted.From, Is.EqualTo(_from));
            Assert.That(converted.To, Is.EqualTo(_to));
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newPeriod = new TimePeriodObject(new TimePeriod());

        Assert.Multiple(() =>
        {
            Assert.That(newPeriod.From, Is.Null);
            Assert.That(newPeriod.To, Is.Null);
        });
    }
}