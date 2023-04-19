using Aosta.Core.Data;
using Aosta.Core.Database.Enums;
using Aosta.Core.Database.Models;

namespace Aosta.Core.Tests.Models;

[TestFixture]
public class AnimeTests
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
        var id = await _core.CreateLocalContentAsync(new Anime());

        using var realm = _core.GetInstance();

        Assert.Multiple(() =>
        {
            Assert.That(realm.All<Anime>().Count(), Is.EqualTo(1));
            Assert.That(realm.First<Anime>().Title, Is.Empty);
        });
    }

    [Test]
    public void EditAnimeTest()
    {
        using var realm = RealmSetup.CreateNewRealm(InitConfig.OneAnime);
        realm.Write(() => { realm.First<Anime>().Title = "Awesome Title"; });
        Assert.That(realm.First<Anime>().Title, Is.EqualTo("Awesome Title"));
    }

    [Test]
    public void EditContentTypeTest()
    {
        using var realm = RealmSetup.CreateNewRealm(InitConfig.OneAnime);
        Assert.That(realm.First<Anime>().Type, Is.EqualTo(ContentType.Unknown));
        realm.Write(() => { realm.First<Anime>().Type = ContentType.Movie; });
        Assert.That(realm.First<Anime>().Type, Is.EqualTo(ContentType.Movie));
    }
}