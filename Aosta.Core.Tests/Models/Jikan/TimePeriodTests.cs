using Aosta.Core.Data;
using Aosta.Core.Data.Models.Jikan;
using Aosta.Jikan.Models.Response;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class TimePeriodTests
{
    [SetUp]
    public void SetUp()
    {
        _from = new DateTime(2000, 10, 24);
        _to = new DateTime(2001, 1, 8);

        _period = new TimePeriodResponse
        {
            From = _from,
            To = _to
        };
    }

    private DateTime _from;
    private DateTime _to;

    private TimePeriodResponse _period = null!;

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
            Assert.That(converted.From, Is.EqualTo(_period.From.Value.UtcDateTime));
            Assert.That(converted.To, Is.EqualTo(_period.To.Value.UtcDateTime));

            Assert.That(converted.From, Is.EqualTo(_from));
            Assert.That(converted.To, Is.EqualTo(_to));
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newPeriod = new TimePeriodObject(new TimePeriodResponse());

        Assert.Multiple(() =>
        {
            Assert.That(newPeriod.From, Is.Null);
            Assert.That(newPeriod.To, Is.Null);
        });
    }
}