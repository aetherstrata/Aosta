using Aosta.Core.Data.Enums;
using Aosta.Core.Data.Models;

namespace Aosta.Core.Tests.Models;

[TestFixture]
public class AnimeObjectTests
{
    [SetUp]
    public void SetUp()
    {
        _core = RealmSetup.NewInstance();
    }

    private AostaDotNet _core;

    [Test]
    public async Task CreateNewAnimeTest()
    {
        var id = await _core.CreateLocalContentAsync(new AnimeObject());

        using var realm = _core.GetInstance();

        Assert.Multiple(() =>
        {
            Assert.That(realm.All<AnimeObject>().Count(), Is.EqualTo(1));
            Assert.That(realm.All<AnimeObject>().First().Title, Is.Empty);
        });
    }

    [Test]
    public void EditAnimeTest()
    {
        using var realm = RealmSetup.CreateNewRealm(InitConfig.OneAnime);
        realm.Write(() => { realm.All<AnimeObject>().First().Title = "Awesome Title"; });
        Assert.That(realm.All<AnimeObject>().First().Title, Is.EqualTo("Awesome Title"));
    }

    [Test]
    public void EditContentTypeTest()
    {
        using var realm = RealmSetup.CreateNewRealm(InitConfig.OneAnime);
        Assert.That(realm.All<AnimeObject>().First().Type, Is.EqualTo(ContentType.Unknown));
        realm.Write(() => { realm.All<AnimeObject>().First().Type = ContentType.Movie; });
        Assert.That(realm.All<AnimeObject>().First().Type, Is.EqualTo(ContentType.Movie));
    }
}