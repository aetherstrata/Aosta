// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Aosta.Data.Database.Models;
using Aosta.Data.Extensions;
using Aosta.Data.Models.Embedded;
using Aosta.Data.Models.Jikan;

using Realms;

using Anime = Aosta.Data.Models.Anime;

namespace Aosta.Data.Tests.Realm;

[TestFixture]
public class AccessorTests
{
    private RealmAccess _realm = null!;

    [SetUp]
    public void Setup()
    {
        _realm = RealmSetup.NewInstance();
    }

    [Test]
    public void differentAccessors_haveDifferentRealms()
    {
        var a1 = RealmSetup.NewInstance();
        var a2 = RealmSetup.NewInstance();

        a1.AddAnime();

        a1.Run(r => r.All<Anime>().Count()).Should().Be(1);
        a2.Run(r => r.All<Anime>().Count()).Should().Be(0);
    }

    [Test]
    [TestCase(0, new long[]{ })]
    [TestCase(0, new long[]{ 5, 34, 56, 76 })]
    [TestCase(1, new long[]{ 54 })]
    [TestCase(1, new long[]{ 57 })]
    [TestCase(1, new long[]{ 1, 45, 54, 67, 88 })]
    [TestCase(1, new long[]{ 1, 45, 57, 67, 88 })]
    [TestCase(2, new long[]{ 54, 57 })]
    [TestCase(2, new long[]{ 32, 54, 57, 67 })]
    public void valueInListQuery_returnsCorrectData(int expected, long[] ids)
    {
        _realm.AddAnime(new Anime
        {
            Jikan = new JikanAnime
            {
                ID = 54
            }
        });

        _realm.AddAnime(new Anime
        {
            Jikan = new JikanAnime
            {
                ID = 57
            }
        });

        _realm.Run(r => r.All<Anime>().In(x => x.Jikan!.ID, ids).Count()).Should().Be(expected);
    }

    [Test]
    [TestCase(new[]{ long.MinValue, -1, 0, 1, long.MaxValue })]
    [TestCase(new[]{ long.MinValue })]
    [TestCase(new[]{ long.MaxValue })]
    [TestCase(new long[]{ 0 })]
    public void valueNotInListQuery_returnsEmptyResults(long[] ids)
    {
        _realm.AddAnime(new Anime
        {
            Jikan = new JikanAnime
            {
                ID = 54
            }
        });

        _realm.Run(r => r.All<Anime>().In(x => x.Jikan!.ID, ids).Count()).Should().Be(0);
    }

    [Test]
    [TestCase(54)]
    [TestCase(57)]
    public void valueIsEqualQuery_returnsCorrectData(long id)
    {
        _realm.AddAnime(new Anime
        {
            Jikan = new JikanAnime
            {
                ID = 54
            }
        });

        _realm.AddAnime(new Anime
        {
            Jikan = new JikanAnime
            {
                ID = 57
            }
        });

        _realm.Run(r =>
        {
            var q = r.All<Anime>().Is(x => x.Jikan!.ID, id);
            return (q.Count(), q.First().Jikan!.ID);
        }).Should().Be((1, id));
    }
}
