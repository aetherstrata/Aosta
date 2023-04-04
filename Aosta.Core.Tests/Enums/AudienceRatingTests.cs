using Aosta.Core.Data.Enums;
using Aosta.Core.Extensions;
using Aosta.Core.Jikan.Models.Response;

namespace Aosta.Core.Tests.Enums;

[TestFixture]
public class AudienceRatingTests : IEnumTests
{
    [Test]
    public void ValuesChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That((int)AudienceRating.Unknown, Is.EqualTo(-1));
            Assert.That((int)AudienceRating.Everyone, Is.EqualTo(0));
            Assert.That((int)AudienceRating.Children, Is.EqualTo(1));
            Assert.That((int)AudienceRating.Teens, Is.EqualTo(2));
            Assert.That((int)AudienceRating.ViolenceProfanity, Is.EqualTo(3));
            Assert.That((int)AudienceRating.MildNudity, Is.EqualTo(4));
            Assert.That((int)AudienceRating.Hentai, Is.EqualTo(5));
        });
    }

    [Test]
    public void QuantityChangedTest()
    {
        Assert.That(Enum.GetValues<AudienceRating>(), Has.Length.EqualTo(7));
    }

    [Test]
    public void CachedStringsChangedTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(AudienceRating.Unknown.ToStringCached(), Is.EqualTo("Unknown"));
            Assert.That(AudienceRating.Everyone.ToStringCached(), Is.EqualTo("G - All Ages"));
            Assert.That(AudienceRating.Children.ToStringCached(), Is.EqualTo("PG - Children"));
            Assert.That(AudienceRating.Teens.ToStringCached(), Is.EqualTo("PG-13 - Teens 13 or older"));
            Assert.That(AudienceRating.ViolenceProfanity.ToStringCached(), Is.EqualTo("R - 17+ (violence & profanity)"));
            Assert.That(AudienceRating.MildNudity.ToStringCached(), Is.EqualTo("R+ - Mild Nudity"));
            Assert.That(AudienceRating.Hentai.ToStringCached(), Is.EqualTo("Rx - Hentai"));
        });
    }

    [Test]
    public void JikanStringParseTest()
    {
        var converted = new AnimeResponse { MalId = 0, Rating = AudienceRating.Hentai.ToStringCached() }.ToRealmObject();
        Assert.That(converted.AgeRating, Is.EqualTo(AudienceRating.Hentai));
    }

    [Test]
    public void JikanInvalidStringParseTest()
    {
        var converted = new AnimeResponse { MalId = 0, Rating = "Invalid" }.ToRealmObject();
        Assert.That(converted.AgeRating, Is.EqualTo(AudienceRating.Unknown));
    }

    [Test]
    public void JikanNullStringParseTest()
    {
        var converted = new AnimeResponse { MalId = 0, Rating = null}.ToRealmObject();
        Assert.That(converted.AgeRating, Is.EqualTo(AudienceRating.Unknown));
    }
}