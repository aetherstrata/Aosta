using Aosta.Core.Data.Enums;
using ContentObject = Aosta.Core.Data.Models.ContentObject;

namespace Aosta.Core.Tests.Models;

[TestFixture]
public class AnimeObjectTests
{
    private AostaDotNet _core;

    [SetUp]
    public void SetUp()
    {
        _core = new(RealmSetup.NewConfig());
    }

    [Test]
    public async Task CreateNewAnimeTest()
    {
        Guid id = await _core.WriteContentAsync(new ContentObject());

        using var realm = _core.GetInstance();

        Assert.Multiple(() =>
        {
            Assert.That(realm.All<ContentObject>().Count(), Is.EqualTo(1));
            Assert.That(realm.All<ContentObject>().First().Title, Is.Empty);
        });
    }

    [Test]
    public void EditAnimeTest()
    {
        using var realm = RealmSetup.CreateNewRealm(InitConfig.OneContent);
        realm.Write(() => { realm.All<ContentObject>().First().Title = "Awesome Title"; });
        Assert.That(realm.All<ContentObject>().First().Title, Is.EqualTo("Awesome Title"));
    }

    [Test]
    public void EditContentTypeTest()
    {
        using var realm = RealmSetup.CreateNewRealm(InitConfig.OneContent);
        Assert.That(realm.All<ContentObject>().First().Type, Is.EqualTo(ContentType.Unknown));
        realm.Write(() => { realm.All<ContentObject>().First().Type = ContentType.Movie;});
        Assert.That(realm.All<ContentObject>().First().Type, Is.EqualTo(ContentType.Movie));
    }
}