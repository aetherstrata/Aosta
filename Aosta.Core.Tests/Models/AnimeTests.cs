using Aosta.Core.Database.Enums;
using Aosta.Core.Database.Models;
using Aosta.Core.Database.Models.Local;
using Aosta.Core.Extensions;

namespace Aosta.Core.Tests.Models;

[TestFixture]
public class AnimeTests
{
    private AostaDotNet _core = null!;

    [SetUp]
    public void SetUp()
    {
        _core = RealmSetup.NewInstance();
    }

    [Test]
    public void CreateNewAnimeTest()
    {
        _core.AddAnime();

        Assert.Multiple(() =>
        {
            Assert.That(_core.Realm.Run(r => r.All<Anime>().Count()), Is.EqualTo(1));
            Assert.That(_core.Realm.Run(r => r.First<Anime>().DefaultTitle), Is.Null);
        });
    }

    [Test]
    public void EditAnimeTest()
    {
        _core.AddAnime();
        _core.Realm.Write(r => r.First<Anime>().Local = new LocalAnime()
        {
            Title = "Awesome Title"
        });
        Assert.That(_core.Realm.Run(r => r.First<Anime>().DefaultTitle), Is.EqualTo("Awesome Title"));
    }

    [Test]
    public void EditContentTypeTest()
    {
        _core.AddAnime();
        _core.Realm.Write(r => r.First<Anime>().Local = new LocalAnime()
        {
            Type = ContentType.Movie
        });
        Assert.That(_core.Realm.Run(r => r.First<Anime>().Type), Is.EqualTo(ContentType.Movie));
    }
}
