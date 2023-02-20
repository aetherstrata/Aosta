using System.Diagnostics.CodeAnalysis;
using Aosta.Core.Data;
using Aosta.Core.Data.Realm;
using Aosta.Core.Tests.Fixture;
using Realms;

namespace Aosta.Core.Tests;

[TestFixture]
public class AnimeObjectTests
{
    private RealmConfiguration _config = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        RealmFixture.Clean();
    }

    [SetUp]
    public void SetUp()
    {
        _config = RealmFixture.CreateNewConfig();
    }

    [Test]
    public void CreateNewAnimeTest()
    {
        using var realm = RealmFixture.CreateNewRealm();

        realm.Write(() => { realm.Add(new AnimeObject()); });

        Assert.That(realm.All<AnimeObject>().Count(), Is.EqualTo(1));
        Assert.That(realm.First<AnimeObject>().Title, Is.Empty);
    }

    [Test]
    public void EditAnimeTest()
    {
        using var realm = RealmFixture.CreateNewRealm(_config, InitConfig.OneAnime);
        realm.Write(() => { realm.First<AnimeObject>().Title = "Awesome Title"; });

        Assert.That(realm.First<AnimeObject>().Title, Is.EqualTo("Awesome Title"));
    }

    [Test]
    public void EditContentTypeTest()
    {
        using var realm = RealmFixture.CreateNewRealm(_config, InitConfig.OneAnime);
        Assert.That(realm.First<AnimeObject>().Type, Is.EqualTo(ContentType.Unknown));
        realm.Write(() => { realm.First<AnimeObject>().Type = ContentType.Movie;});
        Assert.That(realm.First<AnimeObject>().Type, Is.EqualTo(ContentType.Movie));
    }
}