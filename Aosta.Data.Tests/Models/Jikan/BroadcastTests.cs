using Aosta.Data.Database.Mapper;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class BroadcastTests
{
    [Test]
    public void ConversionTest_withUtcTimezone()
    {
        var converted = new AnimeBroadcastResponse
        {
            Day = DaysOfWeek.Mondays,
            Time = "07:00",
            Timezone = "Atlantic/Reykjavik",
            String = "Mondays at 7:00 (GMT)"
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Day.Should().Be(DaysOfWeek.Mondays);
        converted.Time.Should().HaveOffset(TimeSpan.FromHours(0));
        converted.Time.Should().HaveHour(7);
    }

    [Test]
    public void ConversionTest_withMoscowTimezone()
    {
        var converted = new AnimeBroadcastResponse
        {
            Day = DaysOfWeek.Sundays,
            Time = "13:00",
            Timezone = "Europe/Moscow",
            String = "Sundays at 13:00 (MSK)"
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Day.Should().Be(DaysOfWeek.Sundays);
        converted.Time.Should().HaveOffset(TimeSpan.FromHours(3));
        converted.Time.Should().HaveHour(13);
    }

    [Test]
    public void ConversionTest_withJstTimezone()
    {
        var converted = new AnimeBroadcastResponse
        {
            Day = DaysOfWeek.Saturdays,
            Time = "01:00",
            Timezone = "Asia/Tokyo",
            String = "Saturdays at 01:00 (JST)"
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Day.Should().Be(DaysOfWeek.Saturdays);
        converted.Time.Should().HaveOffset(TimeSpan.FromHours(9));
        converted.Time.Should().HaveHour(1);
    }

    [Test]
    public void ConversionTest_withNullTimezone()
    {
        var converted = new AnimeBroadcastResponse
        {
            Day = DaysOfWeek.Mondays,
            Time = "07:00",
            Timezone = null,
            String = null
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Day.Should().Be(DaysOfWeek.Mondays);
        converted.Time.Should().HaveOffset(TimeSpan.Zero);
        converted.Time.Should().HaveHour(7);
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newBroadcast = new AnimeBroadcastResponse().ToRealmModel();

        using var _ = new AssertionScope();
        newBroadcast.Day.Should().BeNull();
        newBroadcast.Time.Should().BeNull();
    }
}