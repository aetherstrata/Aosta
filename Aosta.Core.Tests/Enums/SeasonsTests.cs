using Aosta.Core.Data.Enums;
using Aosta.Core.Extensions;
using JikanDotNet;
using NUnit.Framework;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class SeasonsTests
{
    //TODO: fai test sugli enum

    [Test]
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
             Assert.That((int)Seasons.None, Is.EqualTo(0));
             Assert.That((int)Seasons.Winter, Is.EqualTo(1));
             Assert.That((int)Seasons.Spring, Is.EqualTo(2));
             Assert.That((int)Seasons.Summer, Is.EqualTo(4));
             Assert.That((int)Seasons.Fall, Is.EqualTo(8));
        });
    }

    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<Seasons>(), Has.Length.EqualTo(5));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Seasons.None.ToStringCached(), Is.EqualTo("None"));
            Assert.That(Seasons.Winter.ToStringCached(), Is.EqualTo("Winter"));
            Assert.That(Seasons.Spring.ToStringCached(), Is.EqualTo("Spring"));
            Assert.That(Seasons.Summer.ToStringCached(), Is.EqualTo("Summer"));
            Assert.That(Seasons.Fall.ToStringCached(), Is.EqualTo("Fall"));
        });
    }

    [Test]
    public void JikanNullEnumParseTest()
    {
        var content = new Anime { MalId = 0, Season = null }.ToRealmObject();
        Assert.That(content.Season, Is.EqualTo(Seasons.None));
    }

    [Test]
    public void JikanEnumParseTest()
    {
        var content = new Anime { MalId = 0, Season = Season.Winter }.ToRealmObject();
        Assert.That(content.Season, Is.EqualTo(Seasons.Winter));
    }
}
