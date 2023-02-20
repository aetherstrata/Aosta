using Aosta.Core.Data;

namespace Aosta.Core.Tests;

public class ContentTypeTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ValuesChangedTest()
    {
        Assert.That((int)ContentType.TV, Is.EqualTo(0));
        Assert.That((int)ContentType.ONA, Is.EqualTo(1));
        Assert.That((int)ContentType.OVA, Is.EqualTo(2));
        Assert.That((int)ContentType.Special, Is.EqualTo(3));
        Assert.That((int)ContentType.Movie, Is.EqualTo(4));
    }

    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<ContentType>().Length, Is.EqualTo(6));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.That(ContentType.TV.ToStringCached(), Is.EqualTo("TV"));
        Assert.That(ContentType.ONA.ToStringCached(), Is.EqualTo("ONA"));
        Assert.That(ContentType.OVA.ToStringCached(), Is.EqualTo("OVA"));
        Assert.That(ContentType.Special.ToStringCached(), Is.EqualTo("Special"));
        Assert.That(ContentType.Movie.ToStringCached(), Is.EqualTo("Movie"));
    }

    [Test]
    public void StringParseTest()
    {
        Assert.That(Enum.Parse<ContentType>(ContentType.TV.ToStringCached()), Is.EqualTo(ContentType.TV));
    }

    [Test]
    public void InvalidStringParseTest()
    {
        Assert.Throws<ArgumentException>(() => Enum.Parse<ContentType>(string.Empty));
        Assert.Throws<ArgumentException>(() => Enum.Parse<ContentType>("Invalid"));
    }
}