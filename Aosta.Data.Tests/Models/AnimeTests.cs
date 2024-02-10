using Aosta.Data.Enums;
using Aosta.Data.Extensions;
using Aosta.Data.Models.Local;
using Aosta.Data.Tests.Realm;

using FluentAssertions.Execution;

using Anime = Aosta.Data.Models.Anime;

namespace Aosta.Data.Tests.Models;

[TestFixture]
public class AnimeTests
{
    [Test]
    public void CreateNewAnimeTest()
    {
        var realm = RealmSetup.NewInstance().AddAnime();

        using var _ = new AssertionScope();
        realm.Run(r => r.All<Anime>().Count()).Should().Be(1);
        realm.Run(r => r.First<Anime>().DefaultTitle).Should().BeNull();
    }

    [Test]
    public void EditAnimeTest()
    {
        var realm = RealmSetup.NewInstance().AddAnime();

        realm.Write(r => r.First<Anime>().Local = new LocalAnime()
        {
            Title = "Awesome Title"
        });

        using var _ = new AssertionScope();
        realm.Run(r => r.All<Anime>().Count()).Should().Be(1);
        realm.Run(r => r.First<Anime>().DefaultTitle).Should().Be("Awesome Title");
    }

    [Test]
    public void EditContentTypeTest()
    {
        var realm = RealmSetup.NewInstance().AddAnime();

        realm.Write(r => r.First<Anime>().Local = new LocalAnime()
        {
            Type = ContentType.Movie
        });

        using var _ = new AssertionScope();
        realm.Run(r => r.All<Anime>().Count()).Should().Be(1);
        realm.Run(r => r.First<Anime>().Type).Should().Be(ContentType.Movie);
    }
}
