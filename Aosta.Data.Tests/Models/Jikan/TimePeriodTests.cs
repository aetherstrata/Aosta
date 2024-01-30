using Aosta.Data.Database.Mapper;
using Aosta.Jikan.Models.Response;

using FluentAssertions.Execution;

namespace Aosta.Data.Tests.Models.Jikan;

[TestFixture]
public class TimePeriodTests
{
    [Test]
    public void ConversionTest()
    {
        var converted = new TimePeriodResponse
        {
            From = new DateTime(2000, 10, 24, 0, 0, 0, kind: DateTimeKind.Utc),
            To = new DateTime(2001, 1, 8, 0, 0, 0, kind: DateTimeKind.Utc)
        }.ToRealmModel();

        using var _ = new AssertionScope();
        converted.From.Should().HaveYear(2000).And.HaveMonth(10).And.HaveDay(24);
        converted.To.Should().HaveYear(2001).And.HaveMonth(1).And.HaveDay(8);
    }

    [Test]
    public void DefaultValuesTest()
    {
        var newPeriod = new TimePeriodResponse().ToRealmModel();

        using var _ = new AssertionScope();
        newPeriod.From.Should().BeNull();
        newPeriod.To.Should().BeNull();
    }
}
