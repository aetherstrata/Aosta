using Aosta.Jikan;
using Aosta.Jikan.Models.Response;
using Realms;

//using Aosta.Core.Data.RealmObjects.Mal;

//using AnimeObject = Aosta.Core.Data.RealmObjects.AnimeObject;

namespace Aosta.Core.Tests.Models;

public class JikanContentTests
{
    private IJikan _jikan = null!;
    private AnimeResponse _jikanAime = null!;

    /*
    private AnimeObject expected = new AnimeObject
    {
        Type = ContentType.TV,
        Title = "Cowboy Bebop",
        Synopsis = "Crime is timeless. By the year 2071, humanity has expanded across the galaxy, filling the surface of other planets with settlements like those on Earth. These new societies are plagued by murder, drug use, and theft, and intergalactic outlaws are hunted by a growing number of tough bounty hunters.\n\nSpike Spiegel and Jet Black pursue criminals throughout space to make a humble living. Beneath his goofy and aloof demeanor, Spike is haunted by the weight of his violent past. Meanwhile, Jet manages his own troubled memories while taking care of Spike and the Bebop, their ship. The duo is joined by the beautiful con artist Faye Valentine, odd child Edward Wong Hau Pepelu Tivrusky IV, and Ein, a bioengineered Welsh Corgi.\n\nWhile developing bonds and working to catch a colorful cast of criminals, the Bebop crew's lives are disrupted by a menace from Spike's past. As a rival's maniacal plot continues to unravel, Spike must choose between life with his newfound family or revenge for his old wounds.",
        EpisodeCount = 26,
        Source = "Original",
        Score = 0,
        Review = null,
        AiringStatus = AiringStatus.CurrentlyAiring,
        Airing = false,
    };
    */

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _jikan = new JikanConfiguration().Build();
        _jikanAime = _jikan.GetAnimeAsync(1).Result.Data;
    }

    /*
    [Test]
    public void ObjectMapTest()
    {
        var animeObject = _jikanAime.ToAnimeObject();
        Assert.That(animeObject.Title, Is.EqualTo("Cowboy Bebop"));
        Assert.That(animeObject.Airing, Is.False);
        Assert.That(animeObject.EpisodeCount, Is.EqualTo(26));
        Assert.That(animeObject.AiringStatus, Is.EqualTo(AiringStatus.FinishedAiring));
    }

    [Test]
    public void InsertIntoRealm()
    {
        _realm.Write(() =>
        {
            _realm.Add(_jikanAime.ToAnimeObject());
        });

        var inserted = _realm.First<AnimeObject>();

        Assert.That(inserted.Airing, Is.False);
        Assert.That(inserted.AiringStatus, Is.EqualTo(AiringStatus.FinishedAiring));
    }
    */

    private void AreEqual()
    {
    }
}
