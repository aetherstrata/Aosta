using Aosta.Core.Data.Enums;
using Aosta.Core.Tests.Fixture;
using ContentObject = Aosta.Core.Data.Models.ContentObject;

namespace Aosta.Core.Tests;

[TestFixture]
public class AnimeObjectTests
{
    private AostaDotNet _core;

    [SetUp]
    public void SetUp()
    {
        _core = new(RealmFixture.NewConfig());
    }

    [Test]
    public async Task CreateNewAnimeTest()
    {
        Guid id = await _core.WriteContentAsync(new ContentObject());

        using var realm = _core.GetInstance();

        Assert.Multiple(() =>
        {
            Assert.That(realm.All<ContentObject>().Count(), Is.EqualTo(1));
            Assert.That(realm.First<ContentObject>().Title, Is.Empty);
        });
    }

    [Test]
    public void EditAnimeTest()
    {
        using var realm = RealmFixture.CreateNewRealm(InitConfig.OneContent);
        realm.Write(() => { realm.First<ContentObject>().Title = "Awesome Title"; });
        Assert.That(realm.First<ContentObject>().Title, Is.EqualTo("Awesome Title"));
    }

    [Test]
    public void EditContentTypeTest()
    {
        using var realm = RealmFixture.CreateNewRealm(InitConfig.OneContent);
        Assert.That(realm.First<ContentObject>().Type, Is.EqualTo(ContentType.Unknown));
        realm.Write(() => { realm.First<ContentObject>().Type = ContentType.Movie;});
        Assert.That(realm.First<ContentObject>().Type, Is.EqualTo(ContentType.Movie));
    }
}