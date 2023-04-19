using Aosta.Core.Database.Mapper;
using Aosta.Jikan.Enums;
using Aosta.Jikan.Models.Response;
using FluentAssertions.Execution;

namespace Aosta.Core.Tests.Models.Jikan;

[TestFixture]
public class BroadcastTests
{
    [Test]
    public void FullConversionTest()
    {
        var converted = new AnimeBroadcastResponse
        {
            Day = DaysOfWeek.Mondays,
            Time = "07:00",
            Timezone = "UTC+2:00",
            String = "Mondays at 7:00 (CEST)"
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.Day.Should().Be(DaysOfWeek.Mondays);
        converted.String.Should().Be("Mondays at 7:00 (CEST)");
        converted.Time.Should().HaveOffset(TimeSpan.FromHours(2));
        converted.Time.Should().HaveHour(7);
    }

    [Test]
    public void NoTimezoneConversionTest()
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
        converted.String.Should().Be("Mondays at 7:00 (CEST)");
        converted.Time.Should().HaveOffset(TimeSpan.Zero);
        converted.Time.Should().HaveHour(7);
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newBroadcast = new AnimeBroadcastResponse().ToRealmModel();

        using var _ = new AssertionScope();
        newBroadcast.Day.Should().BeNull();
        newBroadcast.String.Should().BeNull();
        newBroadcast.Time.Should().BeNull();
    }
}