using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models.Jikan;
using Aosta.Core.Extensions;
using JikanDotNet;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class BroadcastTests
{
    [SetUp]
    public void SetUp()
    {
        _broadcast = new AnimeBroadcast
        {
            Day = "Mondays",
            Time = "07:00",
            Timezone = "Europe/Rome",
            String = null
        };

        _time = new TimeOnly(7, 0, 0);
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Rome");
        _days = DaysOfWeek.Monday;
    }

    private AnimeBroadcast _broadcast = null!;

    private TimeOnly _time;
    private TimeZoneInfo _timeZone = null!;
    private DaysOfWeek _days;

    [Test]
    public void FullConversionTest()
    {
        var converted = _broadcast.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.LocalTime.HasValue, Is.True);
            Assert.That(converted.UtcTime.HasValue, Is.True);
            Assert.That(converted.String, Is.Empty);
        });

        Assert.Multiple(() =>
        {
            Assert.That(converted.Day, Is.EqualTo(_days));
            Assert.That(converted.LocalTime, Is.EqualTo(_time));
            Assert.That(converted.UtcTime, Is.EqualTo(_time.Add(-_timeZone.BaseUtcOffset)));
            Assert.That(converted.Timezone, Is.EqualTo(_timeZone));
        });
    }

    [Test]
    public void NoTimezoneConversionTest()
    {
        _broadcast.Timezone = null;

        var converted = _broadcast.ToRealmObject();

        Assert.Multiple(() =>
        {
            Assert.That(converted.LocalTime.HasValue, Is.True);
            Assert.That(converted.UtcTime.HasValue, Is.True);
            Assert.That(converted.Timezone, Is.Null);
            Assert.That(converted.String, Is.Empty);
        });

        Assert.Multiple(() =>
        {
            Assert.That(converted.Day, Is.EqualTo(_days));
            Assert.That(converted.LocalTime, Is.EqualTo(_time));
            Assert.That(converted.UtcTime, Is.EqualTo(_time));
        });
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newBroadcast = new BroadcastObject(new AnimeBroadcast());

        Assert.Multiple(() =>
        {
            Assert.That(newBroadcast.Day, Is.EqualTo(DaysOfWeek.None));
            Assert.That(newBroadcast.LocalTime.HasValue, Is.False);
            Assert.That(newBroadcast.UtcTime.HasValue, Is.False);
            Assert.That(newBroadcast.Timezone, Is.Null);
            Assert.That(newBroadcast.String, Is.Empty);
        });
    }
}