using Aosta.Core.Data.Enums;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Enums;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class SeasonsTests : IEnumTests
{
    [Test]
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
             Assert.That((int)GroupSeason.None, Is.EqualTo(0));
             Assert.That((int)GroupSeason.Winter, Is.EqualTo(1));
             Assert.That((int)GroupSeason.Spring, Is.EqualTo(2));
             Assert.That((int)GroupSeason.Summer, Is.EqualTo(4));
             Assert.That((int)GroupSeason.Fall, Is.EqualTo(8));
        });
    }

    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<GroupSeason>(), Has.Length.EqualTo(5));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(GroupSeason.None.ToStringCached(), Is.EqualTo("None"));
            Assert.That(GroupSeason.Winter.ToStringCached(), Is.EqualTo("Winter"));
            Assert.That(GroupSeason.Spring.ToStringCached(), Is.EqualTo("Spring"));
            Assert.That(GroupSeason.Summer.ToStringCached(), Is.EqualTo("Summer"));
            Assert.That(GroupSeason.Fall.ToStringCached(), Is.EqualTo("Fall"));
        });
    }

    [Test]
    public void JikanNullEnumParseTest()
    {
        var content = new AnimeResponse { MalId = 0, Season = null }.ToRealmObject();
        Assert.That(content.Season, Is.EqualTo(GroupSeason.None));
    }

    [Test]
    public void JikanEnumParseTest()
    {
        var content = new AnimeResponse { MalId = 0, Season = Season.Winter }.ToRealmObject();
        Assert.That(content.Season, Is.EqualTo(GroupSeason.Winter));
    }
}
